using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.Tools;

internal static class StringExtentoins
{
    public static string ToLowerFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }

    public static List<PropertyModel> ClassParse(string stringClass)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(stringClass);
        var root = syntaxTree.GetRoot();
        List<PropertyModel> propertyArray = new();

        var classNode = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
        if (classNode != null)
        {
            var className = classNode.Identifier.ValueText;

            var properties = classNode.DescendantNodes().OfType<PropertyDeclarationSyntax>();
            foreach (var property in properties)
            {
                PropertyModel oPropertyReplacementModel = new PropertyModel
                {
                    PropertyName = property.Identifier.ValueText.ToString(),
                    PropertyType = property.Type.ToString()
                };
                propertyArray.Add(oPropertyReplacementModel);
            }
        }
        return propertyArray;
    }
}
