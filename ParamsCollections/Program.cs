// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

// https://github.com/dotnet/csharplang/issues/7700
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/collection-expressions


void PrintWeekDays(params IEnumerable<string> list) 
    => Console.WriteLine(string.Join(", ", list));

PrintWeekDays("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat");


void PrintNumbers(params List<int>[] numbersLists) 
{
    foreach(var numbers in numbersLists) 
    {
        foreach(var number in numbers) 
        {
            Console.WriteLine(number);
        }
    }
}


PrintNumbers(new List<int> {1, 2, 3}, new List<int> {4, 5, 6}, new List<int> {7, 8, 9});
/*
1
2
3
4
5
6
7
8
9
*/

int Sum(params ReadOnlySpan<int> values) => values.ToArray().Sum(x=>x);

Console.WriteLine(Sum(1, 2, 3,4,5,6,7,8,9,10)); // 55