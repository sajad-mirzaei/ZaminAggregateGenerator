using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal class Domain
{
    private readonly List<PropertyReplacementModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    public Domain(string content, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
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
        _content = Method5();
        _content = Method6();
        _content = Method7();
        _content = Method8();
        _content = Method9();
        return _content;
    }
    string Method1()
    {
        //public string FirstName { get; private set; } //EnterNext
        var oldStr = "DomainReplaceEntityProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"    public {a.PropertyType} {a.PropertyName} {{ get; private set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    string Method2()
    {
        //string firstName, string lastName //~EnterNext
        var oldStr = "DomainReplacePrivateEntityConstructorInputProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            newStr.Append("        " + a.PropertyType + " " + a.PropertyName.ToLowerFirstChar() + ",\n");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

    string Method3()
    {
        //FirstName = firstName; LastName = lastName; //~EnterNext
        var oldStr = "DomainReplacePrivateEntityConstructorAssignedProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            newStr.Append("        " + a.PropertyName + " = " + a.PropertyName.ToLowerFirstChar() + ";\n");
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    string Method4()
    {
        //firstName, lastName //~EnterNext
        var oldStr = "DomainReplacePrivateEntityConstructorSendEvent";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            newStr.Append("            " + a.PropertyName.ToLowerFirstChar() + ",\n");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

    string Method5()
    {
        //string firstName, string lastName //~EnterNext
        var oldStr = "DomainReplacePublicEntityCreateInputProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            newStr.Append("        " + a.PropertyType + " " + a.PropertyName.ToLowerFirstChar() + ",\n");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

    string Method6()
    {
        //firstName, lastName //~EnterNext
        var oldStr = "DomainReplacePublicEntityCreateOutputProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            newStr.Append("            " + a.PropertyName.ToLowerFirstChar() + ",\n");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

    string Method7()
    {
        //public string FirstName { get; private set; } //EnterNext
        var oldStr = "DomainReplaceEventCreatedProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"    public {a.PropertyType} {a.PropertyName} {{ get; private set; }}\n";
            newStr.Append(s);
        }
        return _content.Replace(oldStr, newStr.ToString());
    }

    string Method8()
    {
        //string firstName, string lastName //~EnterNext
        var oldStr = "DomainReplaceEventCreatedConstructorInuptProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            newStr.Append("        " + a.PropertyType + " " + a.PropertyName.ToLowerFirstChar() + ",\n");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

    string Method9()
    {
        //FirstName = firstName; LastName = lastName; //~EnterNext
        var oldStr = "DomainReplaceEventCreatedConstructorAssignedProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            newStr.Append("        " + a.PropertyName + " = " + a.PropertyName.ToLowerFirstChar() + ";\n");
        }
        return _content.Replace(oldStr, newStr.ToString());
    }
}
