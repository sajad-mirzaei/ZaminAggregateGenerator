using RazorAggregateGenerator.Models;
using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.Services;

internal class AggregatePropertyAdder
{
    private readonly RazorAggregateGeneratorModel _aggregateGeneratorModel;
    private readonly List<PropertyModel> _propertyArray;
    private string _content;

    public AggregatePropertyAdder(
        string content,
        List<PropertyModel> propertyArray,
        RazorAggregateGeneratorModel aggregateGeneratorModel)
    {
        _aggregateGeneratorModel = aggregateGeneratorModel;
        _propertyArray = propertyArray;
        _content = content;
        InitializeMethods();
    }

    internal string AddProperties()
    {
        foreach (var (method, textReplacementModel) in _methods)
        {
            var newStr = new StringBuilder();
            foreach (PropertyModel property in _propertyArray)
            {
                textReplacementModel.PropertyModel = property;
                var s = method.Invoke(textReplacementModel);
                newStr.Append(s);
            }
            _content = _content.Replace(method.Method.Name, StringBuilderTrimEnd(newStr, textReplacementModel));
        }
        return _content;
    }

    public static string StringBuilderTrimEnd(StringBuilder newStr, TextReplacementModel textReplacementModel)
    {
        string result = newStr.ToString();
        if (textReplacementModel.TrimEnd != null)
            result = newStr.ToString().TrimEnd().TrimEnd(textReplacementModel.TrimEnd);
        return result;
    }

    #region Domain-ReplacementText
    /// <summary>Output Sample: public int P1 { get; private set; } </summary>;
    private string DomainReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; private set; }}{m.LineBreak}";
    }

    /// <summary>Output Sample: string firstName, string lastName </summary>;
    private string DomainReplacementText2(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName.ToLowerFirstChar()},{m.LineBreak}";
    }

    /// <summary>Output Sample: FirstName = firstName; LastName = lastName; </summary>;
    private string DomainReplacementText3(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} = {m.PropertyModel.PropertyName.ToLowerFirstChar()};{m.LineBreak}";
    }

    /// <summary>Output Sample: firstName, lastName </summary>;
    private string DomainReplacementText4(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName.ToLowerFirstChar()},{m.LineBreak}";
    }

    /// <summary>Output Sample: string firstName, string lastName </summary>;
    private string DomainReplacementText5(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} {m.PropertyModel.PropertyName.ToLowerFirstChar()},{m.LineBreak}";
    }
    #endregion

    #region ApplicationService-ReplacementText
    /// <summary>Output Sample: createAggregateNameCommand.FirstName </summary>;
    private string ApplicationServiceReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}create{_aggregateGeneratorModel.AggregateName}Command.{m.PropertyModel.PropertyName},{m.LineBreak}";
    }
    #endregion

    #region Contracts-ReplacementText
    /// <summary>Output Sample: public string FirstName { get; set; } </summary>;
    private string ContractsReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; set; }}{m.LineBreak}";
    }
    #endregion

    #region SqlQueries-ReplacementText
    /// <summary>Output Sample: FirstName = c.FirstName, LastName = c.LastName </summary>;
    private string SqlQueriesReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} = c.{m.PropertyModel.PropertyName},{m.LineBreak}";
    }

    /// <summary>Output Sample: entities = entities.WhereIf(dto.FirstName != null, p => p.FirstName.Contains(dto.FirstName)); </summary>;
    private string SqlQueriesReplacementText2(TextReplacementModel m)
    {
        var con = string.Empty;
        switch (m.PropertyModel.PropertyType.TrimEnd('?'))
        {
            case "string":
                con = $"{m.LeftPadding}query = query.WhereIf(dto.{m.PropertyModel.PropertyName} != null, p => p.{m.PropertyModel.PropertyName}.Contains(dto.{m.PropertyModel.PropertyName}));{m.LineBreak}";
                break;
            case "DateTime":
            case "long":
            case "float":
            case "double":
            case "bool":
            case "int":
                con = $"{m.LeftPadding}query = query.WhereIf(dto.{m.PropertyModel.PropertyName} != null, m => m.{m.PropertyModel.PropertyName} == dto.{m.PropertyModel.PropertyName});{m.LineBreak}";
                break;
            default:
                break;
        }
        return con;
    }

    /// <summary>Output Sample: public long Id { get; set; } </summary>;
    private string SqlQueriesReplacementText3(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; set; }}{m.LineBreak}";
    }
    #endregion

    #region Initialize-MethodDelegate
    private delegate string MethodDelegate(TextReplacementModel m);
    private Dictionary<MethodDelegate, TextReplacementModel> _methods = new();
    private void InitializeMethods()
    {
        _methods.Add(DomainReplacementText1, new TextReplacementModel(4));
        _methods.Add(DomainReplacementText2, new TextReplacementModel(8, new char[] { ',' }));
        _methods.Add(DomainReplacementText3, new TextReplacementModel(8));
        _methods.Add(DomainReplacementText4, new TextReplacementModel(12, new char[] { ',' }));
        _methods.Add(DomainReplacementText5, new TextReplacementModel(8, new char[] { ',' }));
        _methods.Add(ApplicationServiceReplacementText1, new TextReplacementModel(12, new char[] { ',' }));
        _methods.Add(ContractsReplacementText1, new TextReplacementModel(4));
        _methods.Add(SqlQueriesReplacementText1, new TextReplacementModel(12, new char[] { ',' }));
        _methods.Add(SqlQueriesReplacementText2, new TextReplacementModel(8));
        _methods.Add(SqlQueriesReplacementText3, new TextReplacementModel(4));
    }
    #endregion
}