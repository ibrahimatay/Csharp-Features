using System.Collections.Concurrent;

namespace ConcurrentCollections;

// System.Collections.Concurrent Namespace
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent?view=net-9.0

static class Program
{
    public static void Main()
    {
        //ConcurrentDictionaryExample();
        //ConcurrentBagExample();
        //ThreadSafeListLikeAsConcurrentList();
    }
    
    static readonly ConcurrentDictionary<int, string> Dictionary = new ConcurrentDictionary<int, string>();
    static void ConcurrentDictionaryExample()
    {
        Parallel.For(0, 100, i =>
        {
            var added = Dictionary.TryAdd(i, $"Value-{i}");
            if (added)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} added key {i}");
            }
        });
        
        Console.WriteLine("\nAll items in dictionary:");
        foreach (var kvp in Dictionary)
        {
            Console.WriteLine($"{kvp.Key} => {kvp.Value}");
        }
    }

    static readonly ConcurrentBag<int> Bag = new ConcurrentBag<int>();
    static void ConcurrentBagExample()
    {
        Parallel.For(0, 100, i =>
        {
            Bag.Add(i);
        });

        Console.WriteLine($"Count: {Bag.Count}");
    }

    static readonly List<int> List = new List<int>();
    static readonly object Locker = new object();
    static void ThreadSafeListLikeAsConcurrentList()
    {
        Parallel.For(0, 100, i =>
        {
            lock (Locker)
            {
                List.Add(i);
            }
        });
        
        Console.WriteLine($"Count: {List.Count}");
    }
}