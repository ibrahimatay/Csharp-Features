
// The lock statement - ensure exclusive access to a shared resource
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/lock 

Singleton instance = Singleton.Instance;

Console.WriteLine(instance.CurrentDateTime);

Task.Factory.StartNew(()=>{
    Singleton instance2 = Singleton.Instance;
    Console.WriteLine(instance2.CurrentDateTime);

    new Task(()=>{
        Thread.Sleep(10000);
    }, TaskCreationOptions.AttachedToParent).Start();
    
});


public class Singleton
{
    private readonly System.Threading.Lock _lock = new();
    private static Singleton instance = null;

    public DateTime CurrentDateTime => _currentDateTime;
    private DateTime _currentDateTime;

    private  Singleton() { }

    public static Singleton Instance 
    {
        get 
        {
            if(_lock) 
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