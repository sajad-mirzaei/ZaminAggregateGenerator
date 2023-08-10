using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ZaminAggregateGenerator.Tools;

internal static class StringClass
{
    static void Parse(string input)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(input);
        var root = syntaxTree.GetRoot();

        var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
        if (classNode != null)
        {
            var className = classNode.Identifier.ValueText;

            var properties = classNode.DescendantNodes().OfType<PropertyDeclarationSyntax>();
            foreach (var property in properties)
            {
                var propertyName = property.Identifier.ValueText.ToString();
                var propertyType = property.Type.ToString();

                Console.WriteLine($"var p = {className}.{propertyName};");
                Console.WriteLine($"var pType = {propertyType};");
            }
        }
    }

    static string AggregateRootProperty(string name, string type)
    {
        return $@"public {type} {name} {{ get; private set; }}";
    }
}
