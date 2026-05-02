// C# 14 – field keyword (Field-Backed Properties)
// https://learn.microsoft.com/en-gb/dotnet/csharp/whats-new/csharp-14#the-field-keyword
// https://devblogs.microsoft.com/dotnet/csharp-14-field-keyword/

// Real-world scenario: a customer account in a banking/e-commerce application.
// The email is normalized, the credit limit cannot be negative, and the name field cannot be left empty.

var account = new CustomerAccount("ibrahim")
{
    Email = "  Ibrahim.Atay@contact  ",
    CreditLimit = 50_000m
};

Console.WriteLine($"Name        : {account.FullName}");
Console.WriteLine($"Email       : {account.Email}");        // normalized
Console.WriteLine($"CreditLimit : {account.CreditLimit:C}");

try
{
    account.CreditLimit = -100m; // validation kicks in
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

public class CustomerAccount(string fullName)
{
    // The primary constructor parameter is used together with the field keyword.
    // An empty name is not accepted.
    public string FullName
    {
        get => field;
        set => field = string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException("Name cannot be empty.", nameof(value))
            : value.Trim();
    } = fullName;

    // Normalization: the email is always stored as trimmed + lowercase.
    // No need to declare a separate private backing field.
    public string Email
    {
        get => field;
        set => field = value?.Trim().ToLowerInvariant() ?? string.Empty;
    }

    // Validation: the credit limit cannot be negative.
    // Since the backing field cannot be accessed from elsewhere in the class,
    // the validation is always enforced (encapsulation is preserved).
    public decimal CreditLimit
    {
        get => field;
        set => field = value >= 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Credit limit cannot be negative.");
    }
}