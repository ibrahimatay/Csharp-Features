using System;
using static System.Math;
using static System.Console;
using static UsingStaticDirective.CommonUtil;

namespace UsingStaticDirective
{
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
