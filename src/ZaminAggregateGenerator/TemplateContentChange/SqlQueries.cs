using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal class SqlQueries
{
    private readonly List<PropertyReplacementModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    public SqlQueries(string content, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        _content = content;
        _propertyArray = propertyArray;
        _aggregateGeneratorModel = aggregateGeneratorModel;
    }
    public string Invoke()
    {
        _content = Method1();
        _content = Method2();
        _content = Method3();
        return _content;
    }
    string Method1()
    {
        //FirstName = c.FirstName, LastName = c.LastName //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"{a.PropertyName} = c.{a.PropertyName},";
            newStr.Append(s + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

    string Method2()
    {
        //entities = entities.WhereIf(dto.FirstName != null, p => p.FirstName.Contains(dto.FirstName)); //EnterNext
        //entities = entities.WhereIf(dto.LastName != null, p => p.LastName.Contains(dto.LastName)); //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncWhereIfConditions";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
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
        return _content.Replace(oldStr, newStr.ToString());
    }

    string Method3()
    {
        //FirstName = c.FirstName, LastName = c.LastName, //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncToPagedDataProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"{a.PropertyName} = c.{a.PropertyName},";
            newStr.Append(s + ",\n");
        }
        return _content.Replace(oldStr, newStr.ToString()); ;
    }
}