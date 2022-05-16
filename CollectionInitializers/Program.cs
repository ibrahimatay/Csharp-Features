using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionInitializers
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-initialize-a-dictionary-with-a-collection-initializer
    /// https://stackoverflow.com/questions/27244132/what-benefits-does-dictionary-initializers-add-over-collection-initializers
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable cities = new Hashtable()
            {
                [34] = "İstanbul",
                [6] = "Ankara",
                [10] = "Balıkesir",
                [17] = "Çanakkale",
                [31] = "Hatay",
            };
            
            Console.WriteLine("Cities:");
            foreach (DictionaryEntry city in cities)
            {
                Console.WriteLine($"City Plate  :{city.Key} - City Name :{city.Value}");
            }

            Console.WriteLine();
            Dictionary<string, int> cultureRoutes = new Dictionary<string, int>()
            {
                ["Lycian Way"] =  540,
                ["St Paul Trail"] = 500,
                ["Evliya Çelebi Way"] = 330
            };
            
            Console.WriteLine("Culture Routes:");
            foreach (KeyValuePair<string, int> cultureRoute in cultureRoutes)  
            {                 
                Console.WriteLine($"{cultureRoute.Key} is a {cultureRoute.Value} km");  
            }

            Console.WriteLine();
            Dictionary<string, string> books = new Dictionary<string, string>()
            {
                ["Peopleware productive projects and teams"] = "Tom DeMarco, Tim Lister",
                ["The Mythical Man-Month"] = "Frederick P. Brooks Jr.",
                ["The Pragmatic Programmer"] = "David Thomas, Andrew Hunt"
            };
            
            Console.WriteLine("Books:");
            foreach (KeyValuePair<string, string> book in books)  
            {                 
                Console.WriteLine($"Name  : {book.Key} Author : {book.Value}");  
            }
        }
    }
}
