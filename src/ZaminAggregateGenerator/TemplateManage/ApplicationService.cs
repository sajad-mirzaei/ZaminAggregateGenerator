using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.TemplateManage;

internal class ApplicationService
{
    private readonly List<PropertyReplacementModel> _propertyArray;
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private string _content;

    private delegate string MethodDelegate();
    private List<MethodDelegate> _methods = new();
    public ApplicationService(string content, List<PropertyReplacementModel> propertyArray, AggregateGeneratorModel aggregateGeneratorModel)
    {
        _content = content;
        _propertyArray = propertyArray;
        _aggregateGeneratorModel = aggregateGeneratorModel;
        InitializeMethods();
    }
    private void InitializeMethods()
    {
        _methods.Add(Method1);
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
        //createAggregateNameCommand.FirstName, createAggregateNameCommand.LastName //~EnterNext
        var oldStr = "ApplicationServiceReplaceHandlerAssignedProperty";
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"            create{_aggregateGeneratorModel.AggregateName}Command.{a.PropertyName},\n";
            newStr.Append(s);
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(oldStr, ns);
    }

}