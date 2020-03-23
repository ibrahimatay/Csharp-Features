﻿using System;

namespace TupleTypes
{
    /// <summary>
    /// Tuple types
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7#tuples
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var foo = (val1: 1, val2: 3);
            var bar = (val1: 1, val2: 3);

            Console.WriteLine(foo == bar); // return of true

            (int a, int b) values1 = (1, 5);
            (int c, int d) values2 = (1, 5);

            Console.WriteLine(values1 == values2); // return of true

            (int x, int y) = (12, 13);

            Console.WriteLine($"Values x:{x} y:{y}");

            int x2;
            int y2;

            (x2, y2) = (1, 2);
            
            Console.WriteLine($"Values x:{x2} y:{y2}");

            (string firstName, string lastName) person01 = ("İbrahim", "ATAY");
            var val01 = GetPersonInfo(person01);

            Console.WriteLine($"GetPersonInfo by {val01.firstName}  {val01.lastName}");
            
            var val02 = OverrideGetPersonInfo01();
            
            Console.WriteLine($"OverrideGetPersonInfo01 by {val02.id} {val02.firstName} {val02.lastName}");
            
            (int id, string firstName, string lastName) person02 = (1, "İbrahim", "ATAY");
            
            var val03 = OverrideGetPersonInfo01(person02);
            
            Console.WriteLine($"OverrideGetPersonInfo01 by {val03.id} {val03.firstName} {val03.lastName}");
            
            var val04 = OverrideGetPersonInfo01(person01);
            
            Console.WriteLine($"OverrideGetPersonInfo01 by {val03.id} {val03.firstName} {val03.lastName}");
        }

        static (string firstName, string lastName) GetPersonInfo(
            (string firstName, string lastName) person)
        {
            return (firstName: person.firstName, lastName: person.lastName);
        }
        
        static (int id, string firstName, string lastName) OverrideGetPersonInfo01()
        {
            return (1, firstName: "İbrahim",lastName: "ATAY");
        }

        static (int id, string firstName, string lastName) OverrideGetPersonInfo01(
            (int id, string firstName, string lastName) person)
        {
            return person;
        }
        
        static (int id, string firstName, string lastName) OverrideGetPersonInfo01(
            (string firstName, string lastName) person)
        {
            return (1, firstName: person.firstName, lastName: person.lastName);
        }
    }
}