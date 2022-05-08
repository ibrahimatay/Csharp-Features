using System;

namespace NullCoalescing
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string? name = null;
            name = name ?? "Hello";
            name ??= "Hello"; // C# 8.0

            MessageWriter(null, "Recover Message");
            MessageWriter("Hello", "Recover Message");

            var city = new City
            {
                Name = null
            };

            Console.WriteLine(city);

            //CharacterCount(null);
            Console.WriteLine(CharacterCount("Hello"));

        }

        static void MessageWriter<T>(T message, T recoverMessage)
        {
            Console.WriteLine(message ?? recoverMessage);
        }

        static int CharacterCount(string name)
        {
            _ = name ?? throw new ArgumentException(nameof(name));

            return name.Length;
        }
    }

    class City
    {
        public string Name { get; set; }
        public override string ToString() => this.Name;
        //public override string ToString() => this.Name ?? string.Empty;
    }
}
