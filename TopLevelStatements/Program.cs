using System;

Console.WriteLine("Hello Console!");
Console.WriteLine(GetHelloStatement("İbrahim"));

static string GetHelloStatement(string name)=> $"Hello {name}, \nWelcome Top-level Statements feature.";