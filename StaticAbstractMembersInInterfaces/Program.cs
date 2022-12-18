namespace StaticAbstractMembersInInterfaces
{
    /// <summary>
    /// https://stackoverflow.com/questions/3284/why-cant-i-have-abstract-static-methods-in-c
    /// https://blog.ndepend.com/c-11-static-abstract-members/
    /// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members
    /// </summary>
    
    public interface IHandler
    {
        static abstract string Template { get; }
        static abstract string Method { get; }
        static abstract string Handle { get; }
    }

    public record struct HelloWorld : IHandler
    {
        public static string Method => "GET";
        public static string Template => "/";
        public static string Handle => "Hello World";

        public override string ToString()
        {
            return $"Method:{Method} Template:{Template} Handle:{Handle}";
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            HelloWorld hello = new HelloWorld();
            
            Console.WriteLine(hello);
        }
    }
}

