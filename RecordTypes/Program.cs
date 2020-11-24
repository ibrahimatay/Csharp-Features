using System;

namespace RecordTypes
{
    /// <summary>
    /// Record types
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types
    /// https://www.thomasclaudiushuber.com/2020/09/01/c-9-0-records-work-with-immutable-data-classes/
    /// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-9.0/records.md
    /// https://github.com/dotnet/csharplang/issues/2850
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var friend = new Friend("İbrahim", "ATAY");

            Console.WriteLine(friend.FirstName); // İbrahim

            var (first, last) = friend; // deconstruct
            Console.WriteLine(first); // İbrahim
            Console.WriteLine(last); // ATAY

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

        public record Weight(double value);

        public record Friend(string FirstName, string LastName);
        
        public record Person
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }
        }

        public record Developer : Person
        {
            public string FavoriteLanguage { get; init; }
        }
    }
}