
// Default values for parameters in lambda expressions
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-12#default-lambda-parameters

var incrementValue = (int source, int increment) => source + increment;
var incrementValueDefaultParameters = (int source, int increment = 1) => source + increment;

Console.WriteLine(incrementValue(5, 3)); // 8
Console.WriteLine(incrementValue(5, 2)); // 7
Console.WriteLine(incrementValueDefaultParameters(5)); // 6

var addition = (int val1, int val2) => val1 + val2;
var sum = (params int[] values) =>
{
    int sum = 0;
    foreach (var value in values)
    {
        sum = addition(sum, value);
    }

    return sum;
};

var empty = sum();
Console.WriteLine(empty); // 0

var sequence = new[] { 1, 2, 3, 4, 5 };
var total = sum(sequence);

Console.WriteLine(total); // 15

