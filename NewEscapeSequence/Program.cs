// See https://aka.ms/new-console-template for more information

// New Escape Sequence
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#new-escape-sequence

/*
You can use \e as a character literal escape sequence for the ESCAPE character, Unicode U+001B. Previously, you used \u001b or \x1b. Using \x1b wasn't recommended because if the next characters following 1b were valid hexadecimal digits, those characters became part of the escape sequence.
*/


char escapeChar1 = '\u001B'; // Using Unicode escape sequence.
char escapeChar2 = '\x1B';   // Using hexadecimal escape sequence.

char escapeChar = '\e'; // Using the new escape sequence.

Console.WriteLine(escapeChar1);
Console.WriteLine(escapeChar2);
Console.WriteLine(escapeChar);
