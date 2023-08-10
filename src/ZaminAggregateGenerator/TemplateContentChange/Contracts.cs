using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal static class Contracts
{
    static string ContractsReplaceCreateAggregatePropertyCommand(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //public string FirstName { get; set; } = string.Empty //EnterNext
        var oldStr = "ContractsReplaceCreateAggregatePropertyCommand";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return input.Replace(oldStr, newStr.ToString());
    }
    static string ContractsReplaceAggregateGetByIdDtoProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceAggregateGetByIdDtoProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return input.Replace(oldStr, newStr.ToString());
    }

    static string ContractsReplaceAggregateGetDtoProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceAggregateGetDtoProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return input.Replace(oldStr, newStr.ToString());
    }

    static string ContractsReplaceGetAggregateGetQueryProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceGetAggregateGetQueryProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return input.Replace(oldStr, newStr.ToString());
    }

}