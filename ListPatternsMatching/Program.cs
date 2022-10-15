// See https://aka.ms/new-console-template for more information

// https://thecodeblogger.com/2022/09/19/c-11-introducing-list-patterns-matching/
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns
// https://timdeschryver.dev/blog/pattern-matching-examples-in-csharp#relational-patterns
// https://stackoverflow.com/questions/70901344/c-sharp-pattern-match-arrays
// https://gsferreira.com/archive/2022/a-gentle-introduction-to-csharp-11-list-patterns/
// https://endjin.com/blog/2022/02/pattern-matching-in-csharp
// https://www.youtube.com/watch?v=z7bW83Xr1R0&ab_channel=NickChapsas

int[] fibonacci = { 1, 2, 3, 5, 8 };

// result is false, last number is not matching
var result = fibonacci is [1, 2, 3, 5, 9];

Console.WriteLine("Hello, World!");