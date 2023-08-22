using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator.TemplateManage;

internal class Domain
{
    private readonly List<PropertyReplacementModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    private delegate string MethodDelegate();
    private List<MethodDelegate> _methods = new();
    public Domain(string content, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
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
        _methods.Add(Method5);
        _methods.Add(Method6);
        _methods.Add(Method7);
        _methods.Add(Method8);
        _methods.Add(Method9);
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

    private string Method2()
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

    private string Method3()
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

    private string Method4()
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

    private string Method5()
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

    private string Method6()
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

    private string Method7()
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

    private string Method8()
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

    private string Method9()
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
