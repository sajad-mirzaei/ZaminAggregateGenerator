using RazorAggregateGenerator.Models;
using System.Text;
using ZaminAggregateGenerator.Models;

namespace RazorAggregateGenerator.Services;

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

    #region AppCode-ReplacementText
    /// <summary>Output Sample: public int P1 { get; private set; } </summary>;
    private string AppCodeReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; set; }}{m.LineBreak}";
    }

    /// <summary>Output Sample: FirstName = viewModel.FirstName, </summary>;
    private string AppCodeReplacementText2(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} = viewModel.{m.PropertyModel.PropertyName},{m.LineBreak}";
    }
    #endregion

    #region Pages-ReplacementText
    /// <summary>Output Sample: &lt;p&gt;@item.FirstName&lt;/p&gt; </summary>;
    private string PagesReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}<p>@item.{m.PropertyModel.PropertyName}</p>{m.LineBreak}";
    }

    /// <summary>Output Sample: &lt;p&gt;@item.FirstName&lt;/p&gt; </summary>;
    private string PagesReplacementText2(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} = \"{m.PropertyModel.PropertyName}\",{m.LineBreak}";
    }

    /// <summary>Output Sample: FirstName = "FirstName", </summary>;
    private string PagesReplacementText3(TextReplacementModel m)
    {
        var con = string.Empty;
        switch (m.PropertyModel.PropertyType.TrimEnd('?'))
        {
            case "string":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = \"{m.PropertyModel.PropertyName}\",{m.LineBreak}";
                break;
            case "DateTime":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = \"{DateTime.Now}\",{m.LineBreak}";
                break;
            case "long":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = 1,{m.LineBreak}";
                break;
            case "float":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = 1,{m.LineBreak}";
                break;
            case "double":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = 1,{m.LineBreak}";
                break;
            case "bool":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = true,{m.LineBreak}";
                break;
            case "int":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = 1,{m.LineBreak}";
                break;
            case "byte":
                con = $"{m.LeftPadding}{m.PropertyModel.PropertyName} = 1,{m.LineBreak}";
                break;
            default:
                break;
        }
        return con;
    }
    #endregion


    #region Initialize-MethodDelegate
    private delegate string MethodDelegate(TextReplacementModel m);
    private Dictionary<MethodDelegate, TextReplacementModel> _methods = new();
    private void InitializeMethods()
    {
        _methods.Add(AppCodeReplacementText1, new TextReplacementModel(4));
        _methods.Add(AppCodeReplacementText2, new TextReplacementModel(12, new char[] { ',' }));

        _methods.Add(PagesReplacementText1, new TextReplacementModel(16));
        _methods.Add(PagesReplacementText2, new TextReplacementModel(16, new char[] { ',' }));
        _methods.Add(PagesReplacementText3, new TextReplacementModel(16, new char[] { ',' }));
    }
    #endregion
}