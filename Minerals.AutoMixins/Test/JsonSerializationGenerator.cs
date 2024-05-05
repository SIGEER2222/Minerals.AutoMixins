using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

[Generator]
public class JsonSerializationGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Register a syntax provider that will select the relevant classes
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (node, _) => node is ClassDeclarationSyntax m && m.AttributeLists.Count > 0,
                transform: (ctx, _) => (ClassDeclarationSyntax)ctx.Node)
            .Where(static m => m is not null);

        // Combine the selected classes with the semantic model
        var classWithModel = classDeclarations.Combine(context.CompilationProvider);

        // Generate the source for each class
        context.RegisterSourceOutput(classWithModel, (spc, source) => Execute(spc, source.Left, source.Right));
    }

    private static void Execute(SourceProductionContext context, ClassDeclarationSyntax classDeclaration, Compilation compilation)
    {
        // Generate the source code for the class
        string className = classDeclaration.Identifier.Text;
        string namespaceName = GetNamespace(classDeclaration);
        string source = GenerateSourceCode(className, namespaceName);

        context.AddSource($"{className}_JsonSerialization.cs", SourceText.From(source, Encoding.UTF8));
    }

    private static string GenerateSourceCode(string className, string namespaceName)
    {
        // Here you would add the logic to generate the source code for serialization and deserialization
        // This is a simplified example and does not include the actual implementation
        return $@"
using System.Text.Json;

namespace {namespaceName}
{{
    public partial class {className}
    {{
        public static {className} Deserialize(string json)
        {{
            return JsonSerializer.Deserialize<{className}>(json);
        }}

        public string Serialize()
        {{
            return JsonSerializer.Serialize(this);
        }}
    }}
}}
";
    }


    private static string GetNamespace(ClassDeclarationSyntax classDeclaration)
    {
        SyntaxNode? current = classDeclaration.Parent;
        while (current != null && !(current is NamespaceDeclarationSyntax))
        {
            current = current.Parent;
        }

        if (current is NamespaceDeclarationSyntax namespaceDeclaration)
        {
            return namespaceDeclaration.Name.ToString();
        }

        return "Global"; // Fallback to global namespace
    }
}