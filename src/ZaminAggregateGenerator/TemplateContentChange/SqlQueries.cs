using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal static class SqlQueries
{
    static string SqlQueriesReplaceSelectAsyncProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //FirstName = c.FirstName, LastName = c.LastName //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"{a.PropertyName} = c.{a.PropertyName},";
            newStr.Append(s + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return input.Replace(oldStr, ns);
    }

    static string SqlQueriesReplaceSelectAsyncWhereIfConditions(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //entities = entities.WhereIf(dto.FirstName != null, p => p.FirstName.Contains(dto.FirstName)); //EnterNext
        //entities = entities.WhereIf(dto.LastName != null, p => p.LastName.Contains(dto.LastName)); //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncWhereIfConditions";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = "";
            switch (a.PropertyType)
            {
                case "string":
                    s = $"entities = entities.WhereIf(dto.{a.PropertyName} != null, p => p.{a.PropertyName}.Contains(dto.{a.PropertyName}));";
                    break;
                case "int":
                    s = $"entities = entities.WhereIf(dto.{a.PropertyName} != null, p => p.{a.PropertyName}.Contains(dto.{a.PropertyName}));";
                    break;
                default:
                    break;
            }
            if (s != "")
                newStr.Append(s + "\n");
        }
        return input.Replace(oldStr, newStr.ToString());
    }

    static string SqlQueriesReplaceSelectAsyncToPagedDataProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //FirstName = c.FirstName, LastName = c.LastName, //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncToPagedDataProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"{a.PropertyName} = c.{a.PropertyName},";
            newStr.Append(s + ",\n");
        }
        return input.Replace(oldStr, newStr.ToString()); ;
    }
}