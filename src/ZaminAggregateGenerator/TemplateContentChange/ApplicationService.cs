using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal static class ApplicationService
{
    static string ApplicationServiceReplaceHandlerAssignedProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //createAggregateNameCommand.FirstName, createAggregateNameCommand.LastName //~EnterNext
        var oldStr = "ApplicationServiceReplaceHandlerAssignedProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"create{aggregateGeneratorModel.AggregateName}Command.{a.PropertyName}";
            newStr.Append(s + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return input.Replace(oldStr, ns);
    }

}