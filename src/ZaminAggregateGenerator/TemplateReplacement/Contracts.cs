using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateReplacement;

internal class Contracts
{
    private readonly List<PropertyModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    private delegate string MethodDelegate();
    private List<MethodDelegate> _methods = new();
    public Contracts(string content, List<PropertyModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
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
        //public string FirstName { get; set; } = string.Empty //EnterNext
        var oldStr = "ContractsReplaceCreateAggregatePropertyCommand";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"    public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }
    private string Method2()
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceAggregateGetByIdDtoProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"    public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    private string Method3()
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceAggregateGetDtoProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"    public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    private string Method4()
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceGetAggregateGetQueryProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"    public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

}