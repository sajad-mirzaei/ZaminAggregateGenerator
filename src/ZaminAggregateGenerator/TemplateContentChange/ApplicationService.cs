using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal class ApplicationService
{
    private readonly List<PropertyReplacementModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    public ApplicationService(string content, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        _content = content;
        _propertyArray = propertyArray;
        _aggregateGeneratorModel = aggregateGeneratorModel;
    }
    public string Invoke()
    {
        _content = Method1();
        return _content;
    }
    string Method1()
    {
        //createAggregateNameCommand.FirstName, createAggregateNameCommand.LastName //~EnterNext
        var oldStr = "ApplicationServiceReplaceHandlerAssignedProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"            create{_aggregateGeneratorModel.AggregateName}Command.{a.PropertyName},";
            newStr.Append(s);
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

}