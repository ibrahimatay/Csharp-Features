namespace NewLINQMethodsCsharp13
{
    public record Person(string name);
    public class Program
    {
        public static void Main(string[] args)
        {
            var persons = new List<Person>() { new("p1"), new("p2") };

            // Using a for loop
            for (int i = 0; i < persons.Count; i++)
            {
                var person = persons[i];
                Console.WriteLine($"Index: {i}, Person:{person}");
            }

            // Using the new .NET 9 Index method.
            foreach ((int index, Person person) in persons.Index())
            {
                Console.WriteLine($"Index: {index}, Person:{person}");
            }
        }
    }
}