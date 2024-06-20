
// The lock statement - ensure exclusive access to a shared resource
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock 

// Lock Class
// src/libraries/System.Private.CoreLib/src/System/Threading/Lock.cs
// https://learn.microsoft.com/en-us/dotnet/api/system.threading.lock?view=net-9.0

Singleton instance = Singleton.Instance;

Console.WriteLine(instance.CurrentDateTime);

// What is the difference between Task.Run() and Task.Factory.StartNew()
// https://stackoverflow.com/questions/38423472/what-is-the-difference-between-task-run-and-task-factory-startnew

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


public class Singleton
{
    private readonly System.Threading.Lock _lock = new();
    private static Singleton instance = null;
    public DateTime CurrentDateTime => _currentDateTime;
    private DateTime _currentDateTime;
    private Singleton() { }
    public static Singleton Instance
    {
        get
        {
            if (_lock)
            {
                if (instance == null)
                {
                    instance = new();
                    instance._currentDateTime = DateTime.Now;
                }
            }

            return instance;
        }
    }
}