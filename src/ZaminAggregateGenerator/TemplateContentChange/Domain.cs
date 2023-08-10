using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal static class Domain
{
    static string DomainReplaceEntityProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //public string FirstName { get; private set; } //EnterNext
        var oldStr = "DomainReplaceEntityProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; private set; }}\n";
            newStr.Append(s);
        }
        return input.Replace(oldStr, newStr.ToString());
    }

    static string DomainReplacePrivateEntityConstructorInputProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //string firstName, string lastName //~EnterNext
        var oldStr = "DomainReplacePrivateEntityConstructorInputProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            newStr.Append(a.PropertyType + " " + a.PropertyName.ToLowerFirstChar() + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return input.Replace(oldStr, ns);
    }

    static string DomainReplacePrivateEntityConstructorAssignedProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //FirstName = firstName; LastName = lastName; //~EnterNext
        var oldStr = "DomainReplacePrivateEntityConstructorAssignedProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            newStr.Append(a.PropertyName + " = " + a.PropertyName.ToLowerFirstChar() + ";\n");
        }
        return input.Replace(oldStr, newStr.ToString());
    }

    static string DomainReplacePrivateEntityConstructorSendEvent(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //firstName, lastName //~EnterNext
        var oldStr = "DomainReplacePrivateEntityConstructorSendEvent";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            newStr.Append(a.PropertyName.ToLowerFirstChar() + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return input.Replace(oldStr, ns);
    }

    static string DomainReplacePublicEntityCreateInputProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //string firstName, string lastName //~EnterNext
        var oldStr = "DomainReplacePublicEntityCreateInputProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            newStr.Append(a.PropertyType + " " + a.PropertyName.ToLowerFirstChar() + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return input.Replace(oldStr, ns);
    }

    static string DomainReplacePublicEntityCreateOutputProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //firstName, lastName //~EnterNext
        var oldStr = "DomainReplacePublicEntityCreateOutputProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            newStr.Append(a.PropertyName.ToLowerFirstChar() + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return input.Replace(oldStr, ns);
    }

    static string DomainReplaceEventCreatedProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //public string FirstName { get; private set; } //EnterNext
        var oldStr = "DomainReplaceEventCreatedProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            var s = $"public {a.PropertyType} {a.PropertyName} {{ get; private set; }}\n";
            newStr.Append(s);
        }
        return input.Replace(oldStr, newStr.ToString());
    }

    static string DomainReplaceEventCreatedConstructorInuptProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //string firstName, string lastName //~EnterNext
        var oldStr = "DomainReplaceEventCreatedConstructorInuptProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            newStr.Append(a.PropertyType + " " + a.PropertyName.ToLowerFirstChar() + ",");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return input.Replace(oldStr, ns);
    }

    static string DomainReplaceEventCreatedConstructorAssignedProperty(string input, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        //FirstName = firstName; LastName = lastName; //~EnterNext
        var oldStr = "DomainReplaceEventCreatedConstructorAssignedProperty";
        var newStr = new StringBuilder();
        foreach (var a in propertyArray)
        {
            newStr.Append(a.PropertyName + " = " + a.PropertyName.ToLowerFirstChar() + ";\n");
        }
        return input.Replace(oldStr, newStr.ToString());
    }

    #region Tools
    static string ToLowerFirstChar(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }
    #endregion
}
