using System.Collections.Generic;

namespace TargetTypedNewExpressions
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Student student01 = new(1, "İbrahim", "ATAY");
            Student student02 = new()
            {
                Id = 1,
                FirstName = "İbrahim",
                LastName = "ATAY"
            };

            // Course course01 = new("Computer Engineering");  //compilation error
            Course course02 = new(1, "Digital Circuits");
            course02.AddToStudent(new ());

            student02.AddToCourse(new ());
            student02.AddToCourse(new(1, "Computer Organization"));

            Dictionary<string, int[]> cacheDictionary = new()
            {
                {"Index01", new[] {1, 2, 4, 5}},
                {"Index01", new[] {9, 8, 7, 6}},
                {"Index01", new[] {2, 3, 1, 0}}
            };

            List<Student> students = new()
            {
                new(1,"İbrahim", "ATAY")
            };

            (int x, int y) eastCoordinate = new(26, 45);
            (int x, int y) northCoordinate = new(36, 42);
        }
    }

    class Student
    {
        public Student()
        {
            
        }

        public Student(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            _courses = new();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private readonly List<Course> _courses;

        public void AddToCourse(Course course) => _courses.Add(course);
    }

    class Course
    {
        public Course(int id = 1, string name = "Computer Engineering")
        {
            Id = id;
            Name = name;
            _students = new();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        private readonly List<Student> _students;

        public void AddToStudent(Student student) => _students.Add(student);
    }
}
