// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns

int[] fibonacci = { 1, 2, 3, 5, 8 };

bool result = false;

// result is false, last number is not matching
result = fibonacci is [1, 2, 3, 5, 9];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, 2, 3, 5, 9] = {result}");

// result is false, elements are not at same positions
result = fibonacci is [8, 1, 2, 3, 5];
Console.WriteLine($"[1, 2, 3, 5, 9] is [8, 1, 2, 3, 5] = {result}");

// result is false, length is not matching
result = fibonacci is [1, 2, 3, 5];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, 2, 3, 5] = {result}");

// result is true, elements, positions and length is matching
result = fibonacci is [1, 2, 3, 5, 8];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, 2, 3, 5, 8] = {result}");

// result is false, length not matching
result = fibonacci is [_, _, 3, _];
Console.WriteLine($"[1, 2, 3, 5, 9] is [_, _, 3, _] = {result}");

// result is false, 3 is not at same position
result = fibonacci is [_, _, _, 3, _];
Console.WriteLine($"[1, 2, 3, 5, 9] is [_, _, _, 3, _] = {result}");

// result is false, length is matching but 2 and 3 not at same positions
result = fibonacci is [2, _, 3, _, _];
Console.WriteLine($"[1, 2, 3, 5, 9] is [2, _, 3, _, _] = {result}");

// result is true, single element and its position and length is matching
result = fibonacci is [1, _, _, _, _];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, _, _, _, _] = {result}");

// result is true, multiple elements and their positions and length is matching
result = fibonacci is [1, _, 3, _, _];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, _, 3, _, _] = {result}");

// result is true, as fibonacci ends with element 8
result = fibonacci is [.., 8];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, _, 3, _, _] = {result}");

// result is false, 3 is not second last element
result = fibonacci is [.., 3, _];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, _, 3, _, _] = {result}");

// result is true, as sequence begins with 1 and ends with 8
result = fibonacci is [1, .., 8];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, .., 8] = {result}");

// result is true, as 1 is first element
result = fibonacci is [1, ..];
Console.WriteLine($"[1, 2, 3, 5, 9] is [1, ..] = {result}");

// result is false, as fibonacci ends with element 8
result = fibonacci is [..,  <8];
Console.WriteLine($"[1, 2, 3, 5, 9] is [..,  <8] = {result}");

// result is true, 3 is third element from the ending
result = fibonacci is [.., >= 3, _, _];
Console.WriteLine($"[1, 2, 3, 5, 9] is [.., >= 3, _, _] = {result}");

// result is false, as first element is not greater than 1
result = fibonacci is [>1, .., 8];
Console.WriteLine($"[1, 2, 3, 5, 9] is [>1, .., 8] = {result}");