using System;

namespace DefaultInterfaceMethods
{
    /// <summary>
    /// Default Interface Methods
    /// https://www.infoq.com/articles/default-interface-methods-cs8/
    /// https://github.com/dotnet/csharplang/blob/master/meetings/2018/LDM-2018-10-17.md
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/default-interface-methods
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BookBasket basket = new BookBasket(2);

            //Console.WriteLine(basket.TotalPrice());
            
            PaperBasket paperBasket = new PaperBasket(100, 1.5);

            Console.WriteLine(paperBasket.TotalPrice());
        }
    }

    public interface IBasket
    {
        int Count { get; set; }
        double Price { get; set; }
        public double TotalPrice() => Count * Price;
    }

    public class BookBasket : IBasket
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
        public BookBasket(int count) => Count = count;
        
        public BookBasket(int count, double price)
        {
            Count = count;
            Price = price;
        }
            
        public int Count { get; set; }
        public double Price { get; set; }
    }
    
    public class PaperBasket : IBasket
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
        public PaperBasket(int count) => Count = count;

        public PaperBasket(int count, double price)
        {
            Count = count;
            Price = price;
        }

        public int Count { get; set; }
        
        public double Price { get; set; }
        
        public double TotalPrice() => Count * Price;
    }

    public interface ILogger
    {
        void Log(LogLevel level, string message);
        virtual void Log(Exception exception) => Log(LogLevel.Error, exception.ToString());
    }

    public enum LogLevel
    {
        Error = 0,
        Debug = 1,
        Info = 2
    }
    
    public class ConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine(message);
        }
    }

    public class TelemetryLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine(message);
        }

        public void Log(Exception exception)
        {
            Console.WriteLine("Exception Logger");
        }
    }

    public abstract class Telemetry : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine($"{level} - {message}");
        }
    }
}