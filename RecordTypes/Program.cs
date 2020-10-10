using System;

namespace RecordTypes
{
    /// <summary>
    /// Record types
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types
    /// https://www.thomasclaudiushuber.com/2020/09/01/c-9-0-records-work-with-immutable-data-classes/
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
        }
        
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