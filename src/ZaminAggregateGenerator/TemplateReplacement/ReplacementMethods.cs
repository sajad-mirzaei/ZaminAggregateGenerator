using System.Runtime.CompilerServices;
using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator.TemplateReplacement;

internal class ReplacementMethods
{
    #region Properties
    private readonly AggregateGeneratorModel _aggregateGeneratorModel;
    private readonly List<PropertyModel> _propertyArray;
    //private readonly List<ReplacementTextModel> WordsToReplace;
    private string _content;
    #endregion

    public ReplacementMethods(
        string content,
        List<PropertyModel> propertyArray,
        AggregateGeneratorModel aggregateGeneratorModel)
    {
        _aggregateGeneratorModel = aggregateGeneratorModel;
        _propertyArray = propertyArray;
        _content = content;
        InitializeMethods();

        foreach (PropertyModel a in _propertyArray)
        {
            foreach (var item in _methods)
            {

            }
        }
    }


    #region Domain-ReplacementText
    /// <summary>Output Sample: public int P1 { get; private set; } </summary>;
    private string DomainReplacementText1(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (PropertyModel a in _propertyArray)
        {
            var s = $"{textModel.LeftPadding}public {a.PropertyType} {a.PropertyName} {{ get; private set; }}{textModel.LineBreak}";
            newStr.Append(s);
        }
        return _content.Replace(thisMethodName, newStr.ToString());
    }

    /// <summary>Output Sample: string firstName, string lastName </summary>;
    private string DomainReplacementText2(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (PropertyModel a in _propertyArray)
        {
            newStr.Append($"{textModel.LeftPadding}{a.PropertyType} {a.PropertyName.ToLowerFirstChar()},{textModel.LineBreak}");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(thisMethodName, ns);
    }

    /// <summary>Output Sample: FirstName = firstName; LastName = lastName; </summary>;
    private string DomainReplacementText3(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (PropertyModel a in _propertyArray)
        {
            newStr.Append($"{textModel.LeftPadding}{a.PropertyName} = {a.PropertyName.ToLowerFirstChar()};{textModel.LineBreak}");
        }
        return _content.Replace(thisMethodName, newStr.ToString());
    }

    /// <summary>Output Sample: firstName, lastName </summary>;
    private string DomainReplacementText4(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (PropertyModel a in _propertyArray)
        {
            newStr.Append($"{textModel.LeftPadding}{a.PropertyName.ToLowerFirstChar()},{textModel.LineBreak}");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(thisMethodName, ns);
    }

    /// <summary>Output Sample: string firstName, string lastName </summary>;
    private string DomainReplacementText5(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (PropertyModel a in _propertyArray)
        {
            newStr.Append($"{textModel.LeftPadding}{a.PropertyName} {a.PropertyName.ToLowerFirstChar()},{textModel.LineBreak}");
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(thisMethodName, ns);
    }
    #endregion

    #region ApplicationService-ReplacementText
    /// <summary>Output Sample: createAggregateNameCommand.FirstName </summary>;
    private string ApplicationServiceReplacementText1(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"{textModel.LeftPadding}create{_aggregateGeneratorModel.AggregateName}Command.{a.PropertyName},{textModel.LineBreak}";
            newStr.Append(s);
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(thisMethodName, ns);
    }
    #endregion

    #region Contracts-ReplacementText
    /// <summary>Output Sample: public string FirstName { get; set; } </summary>;
    private string ContractsReplacementText1(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"{textModel.LeftPadding}public {a.PropertyType} {a.PropertyName} {{ get; set; }}{textModel.LineBreak}";
            newStr.Append(s);
        }
        return _content.Replace(thisMethodName, newStr.ToString());
    }
    #endregion

    #region SqlQueries-ReplacementText
    /// <summary>Output Sample: FirstName = c.FirstName, LastName = c.LastName </summary>;
    private string SqlQueriesReplacementText1(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"{textModel.LeftPadding}{a.PropertyName} = c.{a.PropertyName},{textModel.LineBreak}";
            newStr.Append(s);
        }
        var ns = newStr.ToString().TrimEnd().TrimEnd(new char[] { ',' });
        return _content.Replace(thisMethodName, ns);
    }

    /// <summary>Output Sample: entities = entities.WhereIf(dto.FirstName != null, p => p.FirstName.Contains(dto.FirstName)); </summary>;
    private string SqlQueriesReplacementText2(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = "";
            var t = a.PropertyType.TrimEnd('?');
            switch (t)
            {
                case "string":
                    s = $"{textModel.LeftPadding}entities = entities.WhereIf(dto.{a.PropertyName} != null, p => p.{a.PropertyName}.Contains(dto.{a.PropertyName}));{textModel.LineBreak}";
                    break;
                case "DateTime":
                case "long":
                case "float":
                case "double":
                case "bool":
                case "int":
                    s = $"{textModel.LeftPadding}entities = entities.WhereIf(dto.{a.PropertyName} != null, m => m.{a.PropertyName} == dto.{a.PropertyName});{textModel.LineBreak}";
                    break;
                default:
                    break;
            }
            if (s != "")
                newStr.Append(s);
        }
        return _content.Replace(thisMethodName, newStr.ToString());
    }

    /// <summary>Output Sample: public long Id { get; set; } </summary>;
    private string SqlQueriesReplacementText3(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "")
    {
        var newStr = new StringBuilder();
        foreach (var a in _propertyArray)
        {
            var s = $"{textModel.LeftPadding}public {a.PropertyType} {a.PropertyName} {{ get; set; }}{textModel.LineBreak}";
            newStr.Append(s);
        }
        return _content.Replace(thisMethodName, newStr.ToString());
    }
    #endregion

    #region Initialize-MethodDelegate
    private delegate string MethodDelegate(ReplacementTextModel textModel, [CallerMemberName] string thisMethodName = "");
    private List<MethodDelegate> _methods = new();
    private void InitializeMethods()
    {
        //string oldValue, string tabSpace = "    ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => DomainReplacementText1(new ReplacementTextModel(4)));

        //string oldValue, string tabSpace = "        ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => DomainReplacementText2(new ReplacementTextModel(8)));

        //string oldValue, string tabSpace = "        ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => DomainReplacementText3(new ReplacementTextModel(8)));

        //string oldValue, string tabSpace = "            ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => DomainReplacementText4(new ReplacementTextModel(12)));

        //string oldValue, string tabSpace = "        ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => DomainReplacementText5(new ReplacementTextModel(8)));

        //string oldValue, string tabSpace = "            ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => ApplicationServiceReplacementText1(new ReplacementTextModel(12)));

        //string oldValue, string tabSpace = "    ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => ContractsReplacementText1(new ReplacementTextModel(4)));

        //string oldValue, string tabSpace = "            ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => SqlQueriesReplacementText1(new ReplacementTextModel(12)));

        //string oldValue, string tabSpace = "        ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => SqlQueriesReplacementText2(new ReplacementTextModel(8)));

        //string oldValue, string tabSpace = "    ", string enterKey = "\n"
        _methods.Add((textModel, thisMethodName) => SqlQueriesReplacementText3(new ReplacementTextModel(4)));
    }
    #endregion
}