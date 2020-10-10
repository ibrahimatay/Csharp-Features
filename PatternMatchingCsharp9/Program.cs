using System;

namespace PatternMatchingCsharp9
{
    /// <summary>
    /// Pattern matching enhancements
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#pattern-matching-enhancements
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/patterns3
    /// https://github.com/dotnet/csharplang/issues/2850
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Animal cat01 = new Cat();
            Truck truck = new Truck();
            
            if(cat01 is Animal) Console.WriteLine("Cat is animal.");
            if(cat01 is Cat) Console.WriteLine("Cat is cat.");

            //  Program.cs(15, 26): [CS0029]
            // Cannot implicitly convert type 'PatternMatchingEnhancements.Program.Truck' to 'PatternMatchingEnhancements.Program.Animal'
            /*
            if (cat01 is truck) Console.WriteLine("Cat01 is truck");
            if (cat01 is not truck) Console.WriteLine("Cat01 is truck");
            */

            int value = 1;
            if (value > 0 && value <= 10)Console.WriteLine("More than 0 but less than or equal to 10");
            
            // csharp-9.0
            if (value is > 0 and <= 10)Console.WriteLine("More than 0 but less than or equal to 10");
        }

        public class Animal
        {
            protected bool IsAlive { get; set; }
        }
        
        public class Cat : Animal
        {
            public Cat()
            {
                this.IsAlive = true;
            }
        }
        
        public class Truck
        {
            public bool IsWork { get; set; }
        }
    }

    public static class Extensions
    {
        public static bool IsLetter(this char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
        public static bool IsLetterOrSeparator(this char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';
    }
}