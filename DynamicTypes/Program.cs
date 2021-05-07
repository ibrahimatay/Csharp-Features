using System;
using System.Dynamic;

namespace DynamicTypes
{
    /// <summary>
    /// https://github.com/IronLanguages/dlr
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/using-type-dynamic
    /// </summary>
    class Program
    {
        public dynamic Property { get; set; }

        static void Main(string[] args)
        {
            Person person01 = new Person("İbrahim", "ATAY"); // clr
            Console.WriteLine($"CLR Type of Person - Type:{person01.GetType()}");

            // Person person02 = new ExpandoObject(); // compile error because Person is clr object
            
            dynamic person03 = new ExpandoObject(); // dlr
            person03.FirstName = "İbrahim";
            person03.LastName = "ATAY";
            Console.WriteLine($"Dynamic Type of Person - Type:{person03.GetType()}");

            dynamic factory = Activator.CreateInstance(typeof(Factory));
            Console.WriteLine($"Factory is {factory.GetFactoryName()}");
        }

        public static void Sum(int val01) { }
        public static void Sum(dynamic val01) { }
        public static void Sum(int val01, dynamic val02) { }
        public static void Sum(dynamic val01, dynamic val02) { }

        public dynamic Foo()=> new ExpandoObject();
        
        public static dynamic Bar() => 1;

        public object Baz() => new ExpandoObject(); // return by dlr object
    }

    class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName{get; set; }
    }

    class Factory
    {
        public string GetFactoryName() => "Big Factory!";
    }
}
