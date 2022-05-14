using System;

namespace GUIDsWithDifferentFormats
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/system.guid.tostring?view=net-6.0
    /// https://stackoverflow.com/questions/7775439/is-the-format-of-guid-always-the-same/7775466#7775466
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Guid currentGuid = Guid.NewGuid();

            Console.WriteLine($"Default Guid Value:{currentGuid}");
            Console.WriteLine();
            Console.WriteLine("----------------------Formatting--------------------------------");
            

            Console.WriteLine($"D:32 digits separated by hyphens:{currentGuid.ToString("D")}");

            Console.WriteLine($"N:32 digits:{currentGuid.ToString("N")}");

            Console.WriteLine($"B:32 digits separated by hyphens, enclosed in braces:{currentGuid.ToString("B")}");

            Console.WriteLine($"P:32 digits separated by hyphens, enclosed in parentheses:{currentGuid.ToString("P")}");

            Console.WriteLine($"X:Four hexadecimal values enclosed in braces, where the fourth value is a subset of eight hexadecimal values that is also enclosed in braces:{currentGuid.ToString("X")}");
            Console.WriteLine();

            Console.WriteLine("----------------------Parsing-----------------------------------");
            try
            {
                Guid.ParseExact(currentGuid.ToString("D"), "D");
                Console.WriteLine($"{currentGuid.ToString("D")} is parsed the guid type.");
            }
            catch
            {
                Console.WriteLine("Failed1");
            }

            try
            {
                Guid.ParseExact(currentGuid.ToString("N"), "N");
                Console.WriteLine($"{currentGuid.ToString("N")} is parsed the guid type.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed3");
            }

            try
            {
                Guid.ParseExact(currentGuid.ToString("B"), "B");
                Console.WriteLine($"{currentGuid.ToString("B")} is parsed the guid type.");
            }
            catch 
            {
                Console.WriteLine("Failed4");
            }

            try
            {
                Guid.ParseExact(currentGuid.ToString("P"), "P");
                Console.WriteLine($"{currentGuid.ToString("P")} is parsed the guid type.");
            }
            catch 
            {
                Console.WriteLine("Failed5");
            }

            try
            {
                Guid.ParseExact(currentGuid.ToString("X"), "X");
                Console.WriteLine($"{currentGuid.ToString("X")} is parsed the guid type.");
            }
            catch 
            {
                Console.WriteLine("Failed6");
            }
            
            Console.ReadLine();
        }
    }
}
