using System;

namespace InParameterModifier
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/in-parameter-modifier
    /// https://stackoverflow.com/questions/52820372/why-would-one-ever-use-the-in-parameter-modifier-in-c
    /// https://github.com/dotnet/csharplang/issues/1133
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person("Emine", "ATAY");
            Print(person);
        }
        
        static void Print(in Person person)
        {
            //person.FirstName = "Emine"; // compilation error
            //person = new Person("Test", "Test"); // compilation error
            Console.WriteLine(person.ToString());
        }

        readonly struct ImmutableObject
        {
            public readonly long Val01;
            public readonly long Val02;
        }
    }

    record Person(string FirstName, string LastName)
    {
        public override string ToString() => $"{FirstName} {LastName}";
    }
}
