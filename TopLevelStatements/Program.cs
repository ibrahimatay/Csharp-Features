using System;
using System.Linq;

/*
 - https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements
 - https://www.thomasclaudiushuber.com/2020/08/18/c-9-top-level-statements-or-should-i-say-hey-wheres-the-main-method/
 */

Console.WriteLine("Hello Console!");
Console.WriteLine(GetHelloStatement("İbrahim"));

if (args.Any())
{
    foreach (string argument in args)
    {
        Console.WriteLine(argument);
    }
}

static string GetHelloStatement(string name)=> $"Hello {name}, \nWelcome Top-level Statements feature.";