
// Tutorial: Explore C# closed hierarchies
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/closed-hierarchies
// https://github.com/dotnet/docs/blob/main/docs/csharp/whats-new/tutorials/closed-hierarchies.md
// https://github.com/dotnet/csharplang/blob/main/proposals/csharp-15.0/closed-hierarchies.md
// https://github.com/dotnet/docs/issues/54013

const string Iban = "TR130006200000001234567890";

Run("Happy path", new Draft(NewLoan("C-1001", 250_000m, 24)),
[
    new Submit(),
    new AssignUnderwriter("i.Atay"),
    new Decide(CreditScore: 1_620),
    new Disburse(Iban),
]);

Run("Rejected application", new Draft(NewLoan("C-1002", 400_000m, 36)),
[
    new Submit(),
    new AssignUnderwriter("a.kaya"),
    new Decide(CreditScore: 980),
    new Disburse(Iban),            // refused: Rejected is a final state
]);

Run("Invalid transitions", new Draft(NewLoan("C-1003", 50_000m, 12)),
[
    new Disburse(Iban),            // refused: cannot disburse a draft
    new Submit(),
    new Submit(),                  // refused: already submitted
    new Cancel("Customer request"),
    new AssignUnderwriter("k.yilmaz"), // refused: Cancelled is a final state
]);

static LoanInfo NewLoan(string customerId, decimal amount, int termMonths) => new(Guid.CreateVersion7(), customerId, amount, termMonths);

static void Run(string title, LoanApplication state, LoanCommand[] commands)
{
    Console.WriteLine($"\n=== {title} ({state.Ref}, {state.Loan.Amount:N0} TRY / {state.Loan.TermMonths} months) ===");

    foreach (var command in commands)
    {
        switch (LoanWorkflow.Handle(state, command, DateTimeOffset.UtcNow))
        {
            case Moved moved:
                Console.WriteLine($"  OK  {command.GetType().Name,-18} -> {LoanViews.Status(moved.State)}");
                state = moved.State;
                break;
            case Refused refused:
                Console.WriteLine($"  NO  {command.GetType().Name,-18} -> {refused.Reason}");
                break;
        }
    }

    Console.WriteLine($"  Customer message: {LoanViews.CustomerMessage(state)}");
}

public sealed record LoanInfo(Guid Id, string CustomerId, decimal Amount, int TermMonths);
public sealed record Offer(decimal AnnualRate, decimal MonthlyInstallment);

public closed record class LoanApplication(LoanInfo Loan)
{
    // Unlike a union, a closed hierarchy keeps inheritance:
    // shared data and behavior live once in the base type.
    public string Ref => $"LN-{Loan.Id.ToString("N")[^8..].ToUpperInvariant()}";

    public bool IsFinal => this is Rejected or Disbursed or Cancelled;
}

public sealed record Draft(LoanInfo Loan) : LoanApplication(Loan);
public sealed record Submitted(LoanInfo Loan, DateTimeOffset SubmittedAt) : LoanApplication(Loan);
public sealed record UnderReview(LoanInfo Loan, DateTimeOffset SubmittedAt, string Underwriter) : LoanApplication(Loan);

public closed record class Decided(LoanInfo Loan, DateTimeOffset DecidedAt) : LoanApplication(Loan);
public sealed record Approved(LoanInfo Loan, DateTimeOffset DecidedAt, Offer Offer) : Decided(Loan, DecidedAt);
public sealed record Rejected(LoanInfo Loan, DateTimeOffset DecidedAt, string ReasonCode) : Decided(Loan, DecidedAt);

public sealed record Disbursed(LoanInfo Loan, Offer Offer, string TargetIban, DateTimeOffset DisbursedAt) : LoanApplication(Loan);
public sealed record Cancelled(LoanInfo Loan, string Reason, DateTimeOffset CancelledAt) : LoanApplication(Loan);

// ===================== Commands & results (also closed) =====================

public closed record class LoanCommand;
public sealed record Submit : LoanCommand;
public sealed record AssignUnderwriter(string Underwriter) : LoanCommand;
public sealed record Decide(int CreditScore) : LoanCommand;
public sealed record Disburse(string TargetIban) : LoanCommand;
public sealed record Cancel(string Reason) : LoanCommand;

public closed record class TransitionResult;
public sealed record Moved(LoanApplication State) : TransitionResult;
public sealed record Refused(string Reason) : TransitionResult;

// ===================== Workflow =====================

public static class LoanWorkflow
{
    public static TransitionResult Handle(LoanApplication state, LoanCommand command, DateTimeOffset now) =>
        (state, command) switch
        {
            _ when state.IsFinal
                => new Refused($"{state.GetType().Name} is a final state."),

            (Draft d, Submit)
                => new Moved(new Submitted(d.Loan, now)),

            (Submitted s, AssignUnderwriter a)
                => new Moved(new UnderReview(s.Loan, s.SubmittedAt, a.Underwriter)),

            (UnderReview r, Decide decide)
                => new Moved(Underwriting.Decide(r, decide.CreditScore, now)),

            (Approved a, Disburse d)
                => new Moved(new Disbursed(a.Loan, a.Offer, d.TargetIban, now)),

            (Draft or Submitted or UnderReview or Approved, Cancel c)
                => new Moved(new Cancelled(state.Loan, c.Reason, now)),

            _ => new Refused($"{command.GetType().Name} is not allowed in state {state.GetType().Name}."),
        };
}

public static class Underwriting
{
    // Illustrative thresholds and rates; real values come from credit policy.
    public static Decided Decide(UnderReview review, int creditScore, DateTimeOffset now)
    {
        if (creditScore < 1_100)
            return new Rejected(review.Loan, now, "LOW_CREDIT_SCORE");

        var annualRate = creditScore >= 1_500 ? 0.36m : 0.42m;
        return new Approved(review.Loan, now, new Offer(annualRate, Installment(review.Loan, annualRate)));
    }

    // Standard annuity formula: P * r / (1 - (1 + r)^-n)
    private static decimal Installment(LoanInfo loan, decimal annualRate)
    {
        var r = (double)annualRate / 12;
        var payment = (double)loan.Amount * r / (1 - Math.Pow(1 + r, -loan.TermMonths));
        return decimal.Round((decimal)payment, 2);
    }
}

// ===================== Exhaustive views (no catch-all) =====================

public static class LoanViews
{
    // Exhaustive at the TOP level: Decided is handled as a single case using its shared data.
    public static string Status(LoanApplication application) => application switch
    {
        Draft         => "Draft",
        Submitted s   => $"Submitted at {s.SubmittedAt:HH:mm:ss}",
        UnderReview r => $"Under review by {r.Underwriter}",
        Decided d     => $"Decided at {d.DecidedAt:HH:mm:ss}",
        Disbursed d   => $"Disbursed to {Mask(d.TargetIban)}",
        Cancelled c   => $"Cancelled ({c.Reason})",
    };

    // Exhaustive at the LEAF level: Approved + Rejected fully cover Decided
    // ONLY because Decided is closed. Remove `closed` from Decided and this
    // switch no longer compiles (CS8509 is treated as an error).
    public static string CustomerMessage(LoanApplication application) => application switch
    {
        Draft or Submitted or UnderReview
                    => "Your application is in progress.",
        Approved a  => $"Approved: {a.Offer.MonthlyInstallment:N2} TRY/month at {a.Offer.AnnualRate:P0} annual rate.",
        Rejected r  => $"We are unable to offer a loan at this time ({r.ReasonCode}).",
        Disbursed d => $"{d.Loan.Amount:N2} TRY has been transferred to {Mask(d.TargetIban)}.",
        Cancelled   => "Your application has been cancelled.",
    };

    private static string Mask(string iban) => $"{iban[..4]} **** {iban[^4..]}";
}