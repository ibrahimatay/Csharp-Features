
// The lock statement - ensure exclusive access to a shared resource
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock 

// Lock Class
// src/libraries/System.Private.CoreLib/src/System/Threading/Lock.cs
// https://learn.microsoft.com/en-us/dotnet/api/system.threading.lock?view=net-9.0

Singleton instance = Singleton.Instance;

Console.WriteLine(instance.CurrentDateTime);

// What is the difference between Task.Run() and Task.Factory.StartNew()
// https://stackoverflow.com/questions/38423472/what-is-the-difference-between-task-run-and-task-factory-startnew

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
Task.Factory.StartNew(() =>
{
    Singleton instance2 = Singleton.Instance;
    Console.WriteLine(instance2.CurrentDateTime);

    new Task(() =>
    {
        Thread.Sleep(10000);
    }, TaskCreationOptions.AttachedToParent).Start();

    new Task(() =>
    {
        Thread.Sleep(5000);
        Singleton instance2 = Singleton.Instance;
        Console.WriteLine(instance2.CurrentDateTime);
    }, TaskCreationOptions.AttachedToParent).Start();
});
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed


public class Singleton
{
    private static readonly System.Threading.Lock _lock = new();
    private static Singleton instance = null;
    public DateTime CurrentDateTime => _currentDateTime;
    private DateTime _currentDateTime;
    private Singleton() { }
    public static Singleton Instance
    {
        get
        {
            lock (_lock)
            {
                // Logical patterns Csharp-9
                // https://devblogs.microsoft.com/dotnet/c-9-0-on-the-record/#logical-patterns
                if (instance is not null)
                {
                    instance = new();
                    instance._currentDateTime = DateTime.Now;
                }
            }

            return instance;
        }
    }
}