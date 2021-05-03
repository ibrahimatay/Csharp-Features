using System;
using static System.Math;
using static System.Console;
using static UsingStaticDirective.CommonUtil;

namespace UsingStaticDirective
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-static
    /// https://stackoverflow.com/questions/31852389/how-do-i-use-the-c6-using-static-feature
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine(Sqrt(144));
            WriteLine("Hello World!");
            WriteLine(GetMessage());
        }
    }

    static class CommonUtil
    {
        public static string GetMessage() => "Test message from CommonUtil";
    }
}
