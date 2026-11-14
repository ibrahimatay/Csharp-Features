
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-15#union-types
// https://github.com/dotnet/docs/blob/main/docs/csharp/whats-new/tutorials/unions.md

const string Rich = "TR130006200000001234567890";     // C-1001, 500,000 TRY
const string Poor = "TR180006200000009876543210";     // C-1002, 1,250 TRY
const string Frozen = "TR410001000000005555666677";   // C-1003, legal hold
const string Receiver = "TR580004600000001112223334";
const string Blacklisted = "TR730006400000004445556667";
const string Closed = "TR100001200000007778889990";

var service = new TransferService(new Bank(), new FraudEngine(), new FastGateway(), new TransferLimits(PerTransactionMax: 100_000m, DailyMax: 250_000m));

(string Name, TransferRequest Request)[] scenarios =
[
    ("Successful transfer",   new("C-1001", Rich,   Receiver,    "Jane Smith",    1_500m)),
    ("Invalid IBAN",          new("C-1001", Rich,   "TR00000",   "Jane Smith",    1_500m)),
    ("Insufficient balance",  new("C-1002", Poor,   Receiver,    "Jane Smith",    5_000m)),
    ("Frozen account",        new("C-1003", Frozen, Receiver,    "Jane Smith",      100m)),
    ("Someone else's account",new("C-1002", Rich,   Receiver,    "Jane Smith",      100m)),
    ("Per-transaction limit", new("C-1001", Rich,   Receiver,    "Jane Smith",  150_000m)),
    ("Blacklisted receiver",  new("C-1001", Rich,   Blacklisted, "X Ltd",           300m)),
    ("New receiver, high",    new("C-1001", Rich,   Receiver,    "Jane Smith",   25_000m, NewReceiver: true)),
    ("OTP required",          new("C-1001", Rich,   Receiver,    "Jane Smith",   75_000m)),
    ("OTP verified",          new("C-1001", Rich,   Receiver,    "Jane Smith",   75_000m, StepUpVerified: true)),
    ("Receiver bank reject",  new("C-1001", Rich,   Closed,      "John Doe",        250m)),
    ("Gateway timeout",       new("C-1001", Rich,   Receiver,    "TIMEOUT",         250m)),
];

foreach (var (name, request) in scenarios)
{
    TransferOutcome outcome = service.Execute(request);
    Console.WriteLine($"{name,-24} {Report.Code(outcome),-16} {Report.Message(outcome)}");
}

// ===================== Unions =====================

// Account lookup: three explicit states instead of null + an IsFrozen flag
public sealed record ActiveAccount(string Iban, string OwnerId, decimal Balance, decimal TransferredToday);
public sealed record FrozenAccount(string Iban, string Reason);
public sealed record AccountNotFound(string Iban);
public union AccountLookup(ActiveAccount, FrozenAccount, AccountNotFound);

// Fraud decision: each decision carries only its own data
public sealed record Approve(int Score);
public sealed record RequireOtp(int Score);
public sealed record SendToReview(string RuleId);
public sealed record Block(string RuleId);
public union FraudDecision(Approve, RequireOtp, SendToReview, Block);

// FAST gateway response
public sealed record Accepted(string FastReference);
public sealed record Rejected(string Code, string Message);
public sealed record TimedOut(TimeSpan Elapsed);
public union GatewayResponse(Accepted, Rejected, TimedOut);

// ALL possible outcomes of a transfer
public sealed record Completed(string FastReference);
public sealed record ValidationFailed(string[] Errors);
public sealed record AccountUnavailable(string Reason);
public sealed record InsufficientFunds(decimal Available, decimal Requested);
public sealed record LimitExceeded(string Kind, decimal Remaining);
public sealed record FraudBlocked(string RuleId);
public sealed record OtpRequired(Guid ChallengeId);
public sealed record ManualReview(Guid CaseId);
public sealed record ProviderRejected(string Code, string Message);
public sealed record StatusUnknown(Guid TransferId); // timeout: money may have left → NO retry, reconcile instead

public union TransferOutcome(
    Completed, ValidationFailed, AccountUnavailable, InsufficientFunds, LimitExceeded,
    FraudBlocked, OtpRequired, ManualReview, ProviderRejected, StatusUnknown);

// ===================== Domain =====================

public sealed record TransferRequest(
    string CustomerId, string From, string To, string ReceiverName, decimal Amount,
    bool NewReceiver = false, bool StepUpVerified = false);

public sealed record TransferLimits(decimal PerTransactionMax, decimal DailyMax);

public sealed class TransferService(Bank bank, FraudEngine fraud, FastGateway gateway, TransferLimits limits)
{
    // Validation → Account → Limits/Balance → Fraud → Gateway.
    // Each step returns a union; the next step runs only on the "happy" case.
    public TransferOutcome Execute(TransferRequest r)
    {
        string[] errors = Validate(r);
        if (errors.Length > 0) return new ValidationFailed(errors);

        AccountLookup lookup = bank.Find(r.From);
        TransferOutcome outcome = lookup switch
        {
            AccountNotFound                                   => new AccountUnavailable("Account not found."),
            FrozenAccount f                                   => new AccountUnavailable(f.Reason),
            ActiveAccount a when a.OwnerId != r.CustomerId    => new AccountUnavailable("Account does not belong to this customer."),
            ActiveAccount a                                   => CheckLimits(r, a),
        };
        return outcome;
    }

    private TransferOutcome CheckLimits(TransferRequest r, ActiveAccount a)
    {
        if (r.Amount > limits.PerTransactionMax)
            return new LimitExceeded("Per-transaction", limits.PerTransactionMax);

        var remaining = limits.DailyMax - a.TransferredToday;
        if (r.Amount > remaining)
            return new LimitExceeded("Daily", remaining);

        if (r.Amount > a.Balance)
            return new InsufficientFunds(a.Balance, r.Amount);

        FraudDecision decision = fraud.Evaluate(r);
        TransferOutcome outcome = decision switch
        {
            Block b        => new FraudBlocked(b.RuleId),
            SendToReview _ => new ManualReview(Guid.CreateVersion7()),
            RequireOtp _   => new OtpRequired(Guid.CreateVersion7()),
            Approve _      => Send(r),
        };
        return outcome;
    }

    private TransferOutcome Send(TransferRequest r)
    {
        var transferId = Guid.CreateVersion7();
        GatewayResponse response = gateway.Send(r);
        TransferOutcome outcome = response switch
        {
            Accepted ok  => new Completed(ok.FastReference),
            Rejected rej => new ProviderRejected(rej.Code, rej.Message),
            TimedOut     => new StatusUnknown(transferId),
        };
        return outcome;
    }

    private static string[] Validate(TransferRequest r)
    {
        List<string> errors = [];
        if (!Iban.IsValidTr(r.From)) errors.Add("Sender IBAN is invalid.");
        if (!Iban.IsValidTr(r.To)) errors.Add("Receiver IBAN is invalid.");
        if (r.From == r.To) errors.Add("Sender and receiver cannot be the same.");
        if (r.Amount <= 0) errors.Add("Amount must be greater than zero.");
        if (string.IsNullOrWhiteSpace(r.ReceiverName)) errors.Add("Receiver name is required.");
        return [.. errors];
    }
}

// ===================== Outcome consumers =====================
// The same union is consumed by two exhaustive switches.
// Adding a new case to TransferOutcome breaks both at compile time (CS8509 = error).

public static class Report
{
    public static string Code(TransferOutcome o) => o switch
    {
        Completed          => "201 COMPLETED",
        ValidationFailed   => "400 VALIDATION",
        AccountUnavailable => "422 ACCOUNT",
        InsufficientFunds  => "422 FUNDS",
        LimitExceeded      => "422 LIMIT",
        FraudBlocked       => "403 BLOCKED",
        OtpRequired        => "202 OTP",
        ManualReview       => "202 REVIEW",
        ProviderRejected   => "502 REJECTED",
        StatusUnknown      => "202 RECONCILE",
    };

    public static string Message(TransferOutcome o) => o switch
    {
        Completed c          => $"Transfer completed. Ref: {c.FastReference}",
        ValidationFailed v   => string.Join(" ", v.Errors),
        AccountUnavailable a => a.Reason,
        InsufficientFunds f  => $"Insufficient balance. Available {f.Available:N2}, requested {f.Requested:N2}",
        LimitExceeded l      => $"{l.Kind} limit exceeded. Remaining: {l.Remaining:N2}",
        FraudBlocked         => "Blocked for security reasons.",   // rule ID is never shown to the customer
        OtpRequired          => "Approval via mobile app required.",
        ManualReview         => "Transfer is under review.",
        ProviderRejected p   => $"Receiver bank rejected the transfer ({p.Code}). No funds were debited.",
        StatusUnknown        => "Result not yet confirmed. Do not resend.",
    };
}

// ===================== Infrastructure (simulated) =====================

public sealed class Bank
{
    private readonly Dictionary<string, AccountLookup> _accounts = new()
    {
        ["TR130006200000001234567890"] = new ActiveAccount("TR130006200000001234567890", "C-1001", 500_000m, 0m),
        ["TR180006200000009876543210"] = new ActiveAccount("TR180006200000009876543210", "C-1002", 1_250m, 0m),
        ["TR410001000000005555666677"] = new FrozenAccount("TR410001000000005555666677", "Account is under a legal hold."),
    };

    public AccountLookup Find(string iban) =>
        _accounts.TryGetValue(iban, out var account) ? account : new AccountNotFound(iban);
}

public sealed class FraudEngine
{
    public FraudDecision Evaluate(TransferRequest r)
    {
        if (r.To == "TR730006400000004445556667") return new Block("FR-001-BLACKLIST");
        if (r.NewReceiver && r.Amount >= 20_000m) return new SendToReview("FR-014-NEW-RECEIVER");
        if (r.Amount >= 50_000m && !r.StepUpVerified) return new RequireOtp(55);
        return new Approve(10);
    }
}

public sealed class FastGateway
{
    public GatewayResponse Send(TransferRequest r)
    {
        if (r.ReceiverName == "TIMEOUT") return new TimedOut(TimeSpan.FromSeconds(3));
        if (r.To == "TR100001200000007778889990") return new Rejected("RECEIVER_CLOSED", "Receiver account is closed.");
        return new Accepted($"FAST{DateTime.UtcNow:yyyyMMddHHmmss}");
    }
}

public static class Iban
{
    // ISO 13616 mod-97 check (TR: 26 characters)
    public static bool IsValidTr(string iban)
    {
        if (iban.Length != 26 || !iban.StartsWith("TR")) return false;
        var remainder = 0;
        foreach (var ch in iban[4..] + iban[..4])
        {
            if (!char.IsAsciiLetterOrDigit(ch)) return false;
            var d = char.IsAsciiDigit(ch) ? ch - '0' : char.ToUpperInvariant(ch) - 'A' + 10;
            remainder = (remainder * (d >= 10 ? 100 : 10) + d) % 97;
        }
        return remainder == 1;
    }
}