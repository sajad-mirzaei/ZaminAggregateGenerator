using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal class Contracts
{
    private readonly List<PropertyReplacementModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    public Contracts(string content, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
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
        _content = Method4();
        return _content;
    }
    string Method1()
    {
        //public string FirstName { get; set; } = string.Empty //EnterNext
        var oldStr = "ContractsReplaceCreateAggregatePropertyCommand";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }
    string Method2()
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceAggregateGetByIdDtoProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    string Method3()
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceAggregateGetDtoProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    string Method4()
    {
        //public string? FirstName { get; set; } //EnterNext
        var oldStr = "ContractsReplaceGetAggregateGetQueryProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

}