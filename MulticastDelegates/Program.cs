internal class Program
{
    // How to combine delegates (Multicast Delegates) (C# Programming Guide)
    // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/how-to-combine-delegates-multicast-delegates

    // Delegates, Multicast delegates and Events in C# - Notes
    // https://www.codeproject.com/Articles/1061085/Delegates-Multicast-delegates-and-Events-in-Csharp

    static void Hello(string s) => Console.WriteLine("Hello, {0}", s);
    static void GoodBye(string s) => Console.WriteLine("Goodbye, {0}", s);

    private static void Main(string[] args)
    {
        CustomCallback hello, goodbye, multiDelegation;

        hello = new CustomCallback(Hello);
        goodbye = new CustomCallback(GoodBye);

        multiDelegation = hello + goodbye;

        multiDelegation("İbrahim");
    }
}

delegate void CustomCallback(String s);
