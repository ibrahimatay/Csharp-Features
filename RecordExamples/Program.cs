using System;

namespace RecordExamples
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types
    /// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-9.0/records.md
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var person = new Person() { FirstName = "İbrahim", LastName = "ATAY" };

            Console.WriteLine(person);

            var sister = person with { FirstName = "Fulya" };

            Console.WriteLine($"Sister: {sister}");

            // https://stackoverflow.com/questions/63994473/how-to-copy-clone-records-in-c-sharp-9
            var sister2 = sister with { }; // copy/clone records 

            Console.WriteLine($"Clone to Sister: {sister2}");

            Console.WriteLine($"Sister == Clone Sisiter => {sister == sister2}");

            var weight1 = new Weight(1);
            var weight2 = new Weight(1.001);

            Console.WriteLine($"{weight1} == {weight2} => {weight1 == weight2}");

            Console.ReadLine();
        }
    }

    public record Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public record Weight(double value);
}
