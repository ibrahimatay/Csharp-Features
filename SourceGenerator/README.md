# Source Generator

##Project File (csproj) Config


    <ItemGroup>
        <PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="3.8.0-3.final" PrivateAssets="all" />
        <PackageReference Include="Microsoft.CodeAnalysis.Analyzers" Version="3.0.0" PrivateAssets="all" />
        <ProjectReference Include="ClassLibrary1.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
    </ItemGroup>

##Reference
- https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md
- https://github.com/dotnet/roslyn-sdk/tree/main/samples/CSharp/SourceGenerators/SourceGeneratorSamples
- https://docs.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/compiler-api-model