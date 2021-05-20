using System;
using System.Diagnostics;

namespace AttributesOnLocalFunctions
{
    /// <summary>
    /// Attributes on local functions
    /// https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.windowsruntime.returnvaluenameattribute?view=netcore-3.1
    /// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/local-function-attributes
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Foo();
            Bar();
            Baz();

            object[] attributes = typeof(PipelineFactory).GetMethod("GetPipelineName")
                .ReturnTypeCustomAttributes
                .GetCustomAttributes(false);

            foreach (var attribute in attributes)
            {
                if (attribute is ReturnValueIgnoredAttribute returnValue)
                {
                    Console.WriteLine($"DefaultValue: {returnValue.DefaultValue} TypeId: {returnValue.TypeId}");
                }
            }
        }

        [Conditional("DEBUG")]
        static void Foo()
        {
            Console.WriteLine("I running at DEBUG");
        }

        [Conditional("NETCOREAPP3_1")]
        static void Bar()
        {
            Console.WriteLine("I running at NETCOREAPP3_1");
        }

        [Conditional("BETA")]
        static void Baz()
        {
            Console.WriteLine("I running at BETA");
        }
    }

    class PipelineFactory
    {
        [return: ReturnValueIgnored("Transaction Data Pipeline")]
        public string GetPipelineName()
        {
            return "Health Data Pipeline";
        }
    }

    [AttributeUsage(AttributeTargets.ReturnValue, AllowMultiple = false)]
    class ReturnValueIgnoredAttribute : Attribute
    {
        public ReturnValueIgnoredAttribute(string defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public string DefaultValue { get; set; }
    }
}
