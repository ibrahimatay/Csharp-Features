// C# 14 – Exploring extension members
// https://devblogs.microsoft.com/dotnet/csharp-exploring-extension-members/

// Extension members
// https://learn.microsoft.com/en-gb/dotnet/csharp/whats-new/csharp-14#extension-members

// Extension members (C# Programming Guide)
// https://learn.microsoft.com/en-gb/dotnet/csharp/programming-guide/classes-and-structs/extension-methods

// Extension declaration (C# Reference)
// https://learn.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/extension

var list = new List<int>() { 1, 2, 3 };
if (list.IsEmpty)
{
    Console.WriteLine("list is empty");
    return;
}

var empty = IEnumerable<int>.EmptySet;

public static class ListExtensions
{
    // Block that adds new capabilities to the IEnumerable<T> type
    extension<T>(IEnumerable<T> source)
    {
        // Extension Property
        public bool IsEmpty => !source.Any();

        // Extension Method
        public void PrintAll()
        {
            foreach (var item in source)
            {
                Console.WriteLine(item);
            }
        }
    }

    // Static Extension (Called directly via type)
    extension<T>(IEnumerable<T>)
    {
        public static IEnumerable<T> EmptySet => Enumerable.Empty<T>();
    }
}