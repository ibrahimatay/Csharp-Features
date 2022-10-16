// See https://aka.ms/new-console-template for more information


Console.WriteLine("Hello");

public class CustomAttribute : Attribute
{
    public Type Type { get; set; }
    public CustomAttribute(Type type)
    {
        Type = type;
    }
}

[Custom(typeof(Student))]
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}


public class GenericCustomAttribute<T> : Attribute 
    where T : class
{
    public T Type { get; set; }
    
}

[GenericCustomAttribute<GenericStudent>()]
public class GenericStudent
{
    public int Id { get; set; }
    public string Name { get; set; }
}