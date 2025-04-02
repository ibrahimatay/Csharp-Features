# Csharp Feature Examples

![.NET Core](https://github.com/ibrahimatay/CsharpLangExamples/workflows/.NET%20Core/badge.svg)

Here are examples of C# programming to help you stay up-to-date with the latest language versions. These examples provide a comprehensive overview of C#'s features and capabilities and serve as a valuable resource for anyone looking to learn or improve their skills in this programming language.

****Note***: Continuous improvements and bug fixes are made within the repository to produce better solutions.*

## Version History

* C# 13 is supported on .NET 9 (September, 2024)
 
| Feature                                                                                                       | Description                                                                                                                                                                                                                        |
|---------------------------------------------------------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Params Collections](ParamsCollections/Program.cs)                                                            | Demonstrates data passing to functions via the params keyword using `IEnumerable`, `List<T>[]`, and `ReadOnlySpan<T>`. Offers a clean and performance-oriented approach for collection-based data processing scenarios.            |
| [New Escape Sequence](NewEscapeSequence/Program.cs)                                                           | Uses `\e`, `\u001B`, and `\x1B` to emit ANSI escape codes for styled console output (underline, blink, RGB color). Demonstrates safer alternatives to `\x1B` and dynamic escape code generation.                                          |
| [Extension Types](ExtensionTypesCsharp13/Program.cs)                                                          | Demonstrates extension methods in C# 3.0 and the upcoming C# 13. Highlights the new implicit extension feature, enabling scoped instance and static member extensions. It includes bitwise accessors using modern syntax for `ulong`. |
| [The lock statement](TheLockStatement/Program.cs)                                                             | Singleton pattern with concurrency control using `System.Threading.Lock`. Demonstrates task lifecycle management via nested Task instances with `AttachedToParent` and instance checks using C# 9 `is not null` pattern matching.      |
| [New LINQ Methods](NewLINQMethodsCsharp13/Program.cs) <br>* `Index()` <br>* `CountBy()` <br>* `AggregateBy()` |Demonstrates advanced LINQ operations using `Index`, `CountBy`, and `AggregateBy` extensions for indexed iteration, grouped counting, and key-based aggregation over collections. Enables more expressive and composable query patterns.                                                                                                                                                                                                                                    |
| [CreateUnboundedPrioritized](CreateUnboundedPrioritized/Program.cs)                                           |Introduced in .NET 9, `Channel.CreateUnboundedPrioritized` enables a priority-based, unbounded, thread-safe message queue. This sample demonstrates prioritized, asynchronous message processing—ideal for real-time, concurrent workloads.                                                                                                                                                                                                                                    |

* C# 12 is supported on .NET 8 (November, 2023) 

|                                          Feature                                          | Description                                                                                                                                                                                                                                                                   |
|-----------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|                        [Keyed Services](KeyedServices/Program.cs)                         | The Keyed Service feature in ASP.NET Core allows multiple implementations of the same interface (such as SMS and Email services) to be registered with unique keys and resolved dynamically at runtime based on those keys.                                                   |
|                  [Primary constructors](PrimaryConstructors/Program.cs)                   | A primary constructor defines parameters directly in the class signature, promoting them into the instance scope. This eliminates the need for explicit field declarations and assignments within the constructor, simplifying the code and enabling more direct data access. |
| [Default values for parameters in lambda expressions](DefaultLambdaParameters/Program.cs) |Lambda expressions enable flexible parameter handling using default arguments and the params keyword, eliminating the need for method overloading. Variadic operations can be handled through single expression definitions.                                                                                                                                                                                                                                                                               |


* C# 11 is supported on .NET 7 (November, 2022)
  * [Raw string literals](RawStringLiterals/Program.cs) 
  * [List Patterns Matching](ListPatternsMatching/Program.cs)
  * [Generic Attributes](GenericAttributes/Program.cs)
  * [File-local types](FileLocalTypes/Program.cs)
  * [Static Abstract Members In Interfaces](StaticAbstractMembersInInterfaces/Program.cs)
  * [User-defined explicit and implicit conversion operators](UserDefinedConversionOperators/Program.cs)

* C# 10 is supported on .NET 6 (November, 2021)
  * [Global using directive](GlobalUsingDirective/Program.cs) 
  * API Improvements
    * [New LINQ Methods](NewLINQMethodsCsharp10/Program.cs)
      * `Chunk()` 
      * `DistinctBy()` 
      * `Take()` 

* C# 9 is supported on .NET 5 (November, 2020)
  * [Target-typed new expressions ](TargetTypedNewExpressions/Program.cs) 
  * [Pattern matching](PatternMatchingCsharp9/Program.cs) 
  * [Record types](RecordTypes/Program.cs) 
  * [Top-level statements](TopLevelStatements/Program.cs) 
  * [Source generator](SourceGenerator/)
  * [Attributes on local functions](AttributesOnLocalFunctions/Program.cs)
    
* C# 8 is supported on .NET Standard 2.1, .NET Core 3.0-3.1 (September 2019)
  * [Asynchronous streams](AsynchronousStreams/Program.cs) 
  * [Null-Coalescing operator](NullCoalescing/Program.cs) 
  * [Default interface methods](DefaultInterfaceMethods/Program.cs) 
  * [Static local functions](StaticLocalFunctions/Program.cs)  
  * [Pattern matching](PatternMatching/Program.cs)
    
* C# 7.1-2-3 is supported on .NET Framework 4.6-4.8, .NET Core 2.1-2.2 (May, 2018)
  * [Attributes on local functions](AttributesOnLocalFunctions/Program.cs) 
  * [Local functions](LocalFunctionsCsharp7/Program.cs) 
  * [Pattern matching](PatternMatching/Program.cs) 
  * [Tuple types](TupleTypes/Program.cs) 
  * [Numeric literal syntax improvement](NumericLiteralSyntaxImprovements/Program.cs)
  * [Immutable collections](ImmutableCollections/Program.cs) 
  * [in parameter modifier](InParameterModifier/Program.cs)
  * [ArrayPool](ArrayPool/Program.cs)
  * [MemoryPool](MemoryPool/Program.cs)
    
* C# 6 is supported on .NET Framework 4.6, .NET Core 1.0-1.1 (July, 2015)
  * [Using static directive](UsingStaticDirective/Program.cs) 
  * [Collection initializers](CollectionInitializers/Program.cs)
    
* C# 4 & others
  * [Dynamic types](DynamicTypes/Program.cs)
  * [Multicast Delegates](MulticastDelegates/Program.cs)
  * [DebuggerDisplay](DebuggerDisplay/Program.cs)

## Notes
- [Which C# version is included in which framework version?](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version)
- [C# Language Features MindMap](https://linkdotnetblogstorage.azureedge.net/blog/20230205_CSharpMindMap/MindMap.svg)
- [.NET release cadence](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [Upgrade Assistant](https://dotnet.microsoft.com/en-us/platform/upgrade-assistant)
- [.NET Source Browser](https://source.dot.net/)








