
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SourceGenerator.Library
{
    [Generator]    
    public class MySourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            
        }

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("myGeneratedFile.cs", SourceText.From(@"
namespace GeneratedLibrary
{
    using System;
    using System.Text;

    public class GeneratedClass
    {
        public void GeneratedMethod()
        {
            Console.WriteLine(DateTime.Now);
            // generated code
        }
    }
}", Encoding.UTF8));
        }
    }
}
