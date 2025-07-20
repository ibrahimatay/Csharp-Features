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
        //ConcurrentQueueExample();
        //ConcurrentStackExample();
        //BlockingCollectionExample();
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
    
    static readonly ConcurrentQueue<int> Queue = new ConcurrentQueue<int>();
    static void ConcurrentQueueExample()
    {
        Parallel.For(0, 100, i =>
        {
            Queue.Enqueue(i);
        });

        Console.WriteLine($"Queue Count After Enqueue: {Queue.Count}");

        while (Queue.TryDequeue(out var result))
        {
            Console.WriteLine($"Dequeued: {result}");
        }
    }

    static readonly ConcurrentStack<int> Stack = new ConcurrentStack<int>();
    static void ConcurrentStackExample()
    {
        Parallel.For(0, 100, i =>
        {
            Stack.Push(i);
        });

        Console.WriteLine($"Stack Count After Push: {Stack.Count}");

        while (Stack.TryPop(out var result))
        {
            Console.WriteLine($"Popped: {result}");
        }
    }

    static readonly BlockingCollection<int> Blocking = new BlockingCollection<int>(boundedCapacity: 10);
    static void BlockingCollectionExample()
    {
        var producer = Task.Run(() =>
        {
            for (int i = 0; i < 20; i++)
            {
                Blocking.Add(i);
                Console.WriteLine($"Produced: {i}");
            }
            Blocking.CompleteAdding();
        });

        var consumer = Task.Run(() =>
        {
            foreach (var item in Blocking.GetConsumingEnumerable())
            {
                Console.WriteLine($"Consumed: {item}");
                Thread.Sleep(100); // Simulate work
            }
        });

        Task.WaitAll(producer, consumer);
    }
}