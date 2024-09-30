using System.Diagnostics;

// Tell the debugger what to show using the DebuggerDisplay Attribute (C#, Visual Basic, F#, C++/CLI)
// https://learn.microsoft.com/en-us/visualstudio/debugger/using-the-debuggerdisplay-attribute?view=vs-2022

//DebuggerDisplayAttribute Class
// https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.debuggerdisplayattribute?view=net-8.0

var person = new Person();

person.Id = 73;
person.Name = "İbrahim";
person.Surname = "ATAY";

var p = person;

// [DebuggerDisplay("{𝘋𝘦𝘣𝘶𝘨𝘨𝘦𝘳𝘋𝘪𝘴𝘱𝘭𝘢𝘺}")]
// [DebuggerDisplay("ID: {Id}: Name:{Name} {Surname}")] // ID: 73: Name:"İbrahim" "ATAY"
// [DebuggerDisplay("{DebuggerMessage}")] // "Id:73 Name: İbrahim ATAY"
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public override string ToString()
    {
        return $"Id:{Id} Name: {Name} {Surname}";
    }

    private string DebuggerMessage
    {
        get
        {
            return $"Id:{Id} Name: {Name} {Surname}";
        }
    }
}
