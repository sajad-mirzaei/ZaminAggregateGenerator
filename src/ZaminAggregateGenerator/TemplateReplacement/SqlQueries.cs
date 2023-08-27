using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateReplacement;

internal class SqlQueries
{
    private readonly List<PropertyModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    private delegate string MethodDelegate();
    private List<MethodDelegate> _methods = new();
    public SqlQueries(string content, List<PropertyModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        _content = content;
        _propertyArray = propertyArray;
        _aggregateGeneratorModel = aggregateGeneratorModel;
        InitializeMethods();
    }
    private void InitializeMethods()
    {
        _methods.Add(Method1);
        _methods.Add(Method2);
        _methods.Add(Method3);
        _methods.Add(Method4);
    }
    public string Exec()
    {
        foreach (MethodDelegate method in _methods)
        {
            _content = method();
        }
        return _content;
    }
    private string Method1()
    {
        //FirstName = c.FirstName, LastName = c.LastName //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"            {a.PropertyName} = c.{a.PropertyName},\n";
            newStr.Append(s);
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

    private string Method2()
    {
        //entities = entities.WhereIf(dto.FirstName != null, p => p.FirstName.Contains(dto.FirstName)); //EnterNext
        //entities = entities.WhereIf(dto.LastName != null, p => p.LastName.Contains(dto.LastName)); //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncWhereIfConditions";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = "";
            var t = a.PropertyType.TrimEnd('?');
            switch (t)
            {
                case "string":
                    s = $"        entities = entities.WhereIf(dto.{a.PropertyName} != null, p => p.{a.PropertyName}.Contains(dto.{a.PropertyName}));\n";
                    break;
                case "DateTime":
                case "long":
                case "float":
                case "double":
                case "bool":
                case "int":
                    s = $"        entities = entities.WhereIf(dto.{a.PropertyName} != null, m => m.{a.PropertyName} == dto.{a.PropertyName});\n";
                    break;
                default:
                    break;
            }
            if (s != "")
                newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    private string Method3()
    {
        //FirstName = c.FirstName, LastName = c.LastName, //EnterNext
        var oldStr = "SqlQueriesReplaceSelectAsyncToPagedDataProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"            {a.PropertyName} = c.{a.PropertyName},\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString()); ;
    }

    private string Method4()
    {
        //public long Id { get; set; } //EnterNext
        var oldStr = "SqlQueriesReplaceModelProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"    public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }
}