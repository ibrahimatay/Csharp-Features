using System;

namespace NumericLiteralSyntaxImprovements
{
    /// <summary>
    /// Numeric literal syntax improvements
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#numeric-literal-syntax-improvements
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Without Using digit separators 
            long x = 100000022200000202; 
            long y = 10000000020; 
  
            Console.WriteLine($"X: {x}"); 
            Console.WriteLine($"Z: {y}");
            
            // Using digit separators 
            long num1 = 1_00_10_00_00_00; 
            var num2 = 0b_010_000_000_000_000_000_000_000_000; 
            long num3 = 1_00_00_00_00_00_00; 
            var num4 = 0b_1_1000_0000_1000_0000_0011_0000_0000_1000_0001;
            var num5 = 0_3.14_00;
            
            Console.WriteLine($"Num1: {num1}"); 
            Console.WriteLine($"Num2: {num2}"); 
            Console.WriteLine($"Num3: {num3}"); 
            Console.WriteLine($"Num4: {num4}");
            Console.WriteLine($"Num4: {num5}");
        }
    }
}