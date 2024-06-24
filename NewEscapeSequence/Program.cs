// See https://aka.ms/new-console-template for more information

// New Escape Sequence
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13#new-escape-sequence

/*
    You can use \e as a character literal escape sequence for the ESCAPE character, 
    Unicode U+001B. Previously, you used \u001b or \x1b. Using \x1b wasn't recommended because 
    if the next characters following 1b were valid hexadecimal digits, those characters became part of the escape sequence.
*/

// ANSI escape code
// https://en.wikipedia.org/wiki/ANSI_escape_code

char escapeChar1 = '\u001B'; // Using Unicode escape sequence.
char escapeChar2 = '\x1B';   // Using hexadecimal escape sequence.
char escapeChar = '\e'; // Using the new escape sequence.

// [small feature] Add \e string escape sequence #7394
// https://github.com/dotnet/csharplang/discussions/7394

Console.WriteLine("\u001b[4m\u001b[6m\u001b[38;2;205;20;60mThis is a bold, blinking, underlined, red text.\u001b[0mThis is a regular text.");
Console.WriteLine("\e[4m\e[6m\e[38;2;205;20;60mThis is a bold, blinking, underlined, red text.\e[0mThis is a regular text.");

static string escape(string code) => $"\u001b[{code}m";
Console.WriteLine($"{escape("4")}{escape("6")}{escape("38;2;205;20;60")}This is a bold, blinking, underlined, red text.{escape("0")}This is a regular text.");
