class Program
{
    /// <summary>
    /// Local functions
    /// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/local-functions
    /// 
    /// What's the benefit of local functions?
    /// https://www.reddit.com/r/csharp/comments/wi1ifu/whats_the_benefit_of_local_functions/
    /// </summary>
    public static void Main()
    {
        int x = 10, y = 5;

        int sum = Sum(x, y);
        int difference = Difference(x, y);

        Console.WriteLine($"The Sum of {x} and {y} is {sum}");
        Console.WriteLine($"The Difference of {x} and {y} is {difference}");

        int Sum(int x, int y)
        {
            return x + y;
        }

        int Difference(int x, int y)
        {
            return x - y;
        }

        Console.ReadKey();
    }
}