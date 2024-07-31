using System;

namespace StaticLocalFunctions
{
    /// <summary>
    /// Static local functions
    /// https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-8#static-local-functions
    /// https://github.com/dotnet/csharplang/blob/master/proposals/csharp-8.0/static-local-functions.md
    /// https://github.com/dotnet/csharplang/issues/1565
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int x = 12;
            int y = 10;

            Console.WriteLine(Extraction(x,y));
            
            static int Extraction(int val1, int val2) => val1 - val2;

            void GenericMessage<Value>(Value x) => Console.WriteLine($"Value from generic message: {x}");

            GenericMessage<int>(12);
            GenericMessage<School>(new School("SAU"));
        }

        int Foo()
        {
            int x = 5;
            int y = 12;

            return Sum(x, y);
            
            static int Sum(int val1, int val2) => val1 + val2;
            
            //Doing so generates CS8421, "A static local function can't contain a reference to <variable>."
            //static School (string name) => new School() {SchoolName = name};
        }
    }

    record School(string SchoolName);
}