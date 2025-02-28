namespace NewLINQMethodsCsharp13
{
    record Student (string Name, string Score);
    public class Program
    {
        public static void Main(string[] args)
        {
            Student[] students = [
                new("Alice", "A"),
                new("Bob", "B"),
                new("Charlie", "C"),
                new("David", "B"),
                new("Eve", "A")
            ];

            Console.WriteLine("----------Index-----------");
            foreach (var (index, student) in students.Index())
            {
                Console.WriteLine($"Student {index}: {student.Name}");
            }

            Console.WriteLine();
            Console.WriteLine("----------CountBy-----------");
            Console.WriteLine();
            foreach (var (score, count) in students.CountBy(student => student.Score))
            {
                Console.WriteLine($"Students with a {score}-score: {count}");
            }
            
            Console.WriteLine();
            Console.WriteLine("----------AggregateBy-----------");
            Console.WriteLine();
            foreach (var (score, studentGroup) in students.AggregateBy(
                         keySelector => keySelector.Score,
                         seed: new List<Student>(),
                         func: (group, student)=> [..group,  student]))
            {
                Console.WriteLine($"Students with a {score}-score: {string.Join(", ", studentGroup)}");
            }
        }
    }
}