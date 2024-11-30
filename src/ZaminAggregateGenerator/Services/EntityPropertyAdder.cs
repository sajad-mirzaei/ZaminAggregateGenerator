using System.Text;
using ZaminAggregateGenerator.Models;

namespace ZaminAggregateGenerator.Services;

internal class EntityPropertyAdder
{
    private readonly EntityGeneratorModel _aggregateGeneratorModel;
    private readonly List<PropertyModel> _propertyArray;
    private string _content;

    public EntityPropertyAdder(
        string content,
        List<PropertyModel> propertyArray,
        EntityGeneratorModel aggregateGeneratorModel)
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
        _content = DisableShadowPropertyReplacement(_content);
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
    private string EntityDomainReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; private set; }}{m.LineBreak}";
    }

    private string EntityDomainReplacementTextEvents1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; set; }}{m.LineBreak}";
    }

    /// <summary>Output Sample: string firstName, string lastName </summary>;
    private string EntityDomainReplacementText2(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName.ToLowerFirstChar()},{m.LineBreak}";
    }

    /// <summary>Output Sample: entity.firstName, entity.lastName </summary>;
    private string EntityDomainReplacementText6(TextReplacementModel m)
    {
        return $"{m.LeftPadding}entity.{m.PropertyModel.PropertyName},{m.LineBreak}";
    }

    /// <summary>Output Sample: FirstName = firstName; LastName = lastName; </summary>;
    private string EntityDomainReplacementText3(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} = {m.PropertyModel.PropertyName.ToLowerFirstChar()};{m.LineBreak}";
    }

    /// <summary>Output Sample: firstName, lastName </summary>;
    private string EntityDomainReplacementText4(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName.ToLowerFirstChar()},{m.LineBreak}";
    }

    /// <summary>Output Sample: string firstName, string lastName </summary>;
    private string EntityDomainReplacementText5(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} {m.PropertyModel.PropertyName.ToLowerFirstChar()},{m.LineBreak}";
    }
    #endregion

    #region ApplicationService-ReplacementText
    /// <summary>Output Sample: createAggregateNameCommand.FirstName </summary>;
    private string EntityApplicationServiceReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}request.{m.PropertyModel.PropertyName},{m.LineBreak}";
    }
    #endregion

    #region Contracts-ReplacementText
    /// <summary>Output Sample: public string FirstName { get; set; } </summary>;
    private string EntityContractsReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; set; }}{m.LineBreak}";
    }
    private string EntityContractsReplacementTextGetQuery1(TextReplacementModel m)
    {
        var propertyType = m.PropertyModel.PropertyType.TrimEnd('?');
        var t = propertyType != "string" ? propertyType + "?" : propertyType;
        return $"{m.LeftPadding}public {t} {m.PropertyModel.PropertyName} {{ get; set; }}{m.LineBreak}";
    }
    #endregion

    #region SqlQueries-ReplacementText
    /// <summary>Output Sample: FirstName = c.FirstName, LastName = c.LastName </summary>;
    private string EntitySqlQueriesReplacementText1(TextReplacementModel m)
    {
        return $"{m.LeftPadding}{m.PropertyModel.PropertyName} = c.{m.PropertyModel.PropertyName},{m.LineBreak}";
    }

    /// <summary>Output Sample: entities = entities.WhereIf(request.FirstName != null, p => p.FirstName.Contains(request.FirstName)); </summary>;
    private string EntitySqlQueriesReplacementText2(TextReplacementModel m)
    {
        var con = string.Empty;
        switch (m.PropertyModel.PropertyType.TrimEnd('?'))
        {
            case "string":
                con = $"{m.LeftPadding}query = query.WhereIf(request.{m.PropertyModel.PropertyName} != null, p => p.{m.PropertyModel.PropertyName}.Contains(request.{m.PropertyModel.PropertyName}));{m.LineBreak}";
                break;
            case "DateTime":
            case "long":
            case "float":
            case "double":
            case "bool":
            case "int":
                con = $"{m.LeftPadding}query = query.WhereIf(request.{m.PropertyModel.PropertyName} != null, m => m.{m.PropertyModel.PropertyName} == request.{m.PropertyModel.PropertyName});{m.LineBreak}";
                break;
            default:
                break;
        }
        return con;
    }

    /// <summary>Output Sample: public long Id { get; set; } </summary>;
    private string EntitySqlQueriesReplacementText3(TextReplacementModel m)
    {
        return $"{m.LeftPadding}public {m.PropertyModel.PropertyType} {m.PropertyModel.PropertyName} {{ get; set; }}{m.LineBreak}";
    }
    #endregion

    #region Initialize-MethodDelegate
    private delegate string MethodDelegate(TextReplacementModel m);
    private Dictionary<MethodDelegate, TextReplacementModel> _methods = new();
    private void InitializeMethods()
    {
        _methods.Add(EntityDomainReplacementText1, new TextReplacementModel(4));
        _methods.Add(EntityDomainReplacementTextEvents1, new TextReplacementModel(4));
        _methods.Add(EntityDomainReplacementText2, new TextReplacementModel(8, new char[] { ',' }));
        _methods.Add(EntityContractsReplacementTextGetQuery1, new TextReplacementModel(4, new char[] { ',' }));
        _methods.Add(EntityDomainReplacementText3, new TextReplacementModel(8));
        _methods.Add(EntityDomainReplacementText4, new TextReplacementModel(12, new char[] { ',' }));
        _methods.Add(EntityDomainReplacementText5, new TextReplacementModel(8, new char[] { ',' }));
        _methods.Add(EntityDomainReplacementText6, new TextReplacementModel(12, new char[] { ',' }));
        _methods.Add(EntityApplicationServiceReplacementText1, new TextReplacementModel(12, new char[] { ',' }));
        _methods.Add(EntityContractsReplacementText1, new TextReplacementModel(4));
        _methods.Add(EntitySqlQueriesReplacementText1, new TextReplacementModel(12, new char[] { ',' }));
        _methods.Add(EntitySqlQueriesReplacementText2, new TextReplacementModel(8));
        _methods.Add(EntitySqlQueriesReplacementText3, new TextReplacementModel(4));
    }
    #endregion

    #region DisableShadowPropertyReplacement
    public string DisableShadowPropertyReplacement(string content)
    {
        if (_aggregateGeneratorModel.DisableShadowProperty == DisableShadowPropertyEnum.True)
        {
            content = content.Replace("DisableShadowPropertyReplacementText1", "");
            content = content.Replace("DisableShadowPropertyReplacementText2", "");
            content = content.Replace("DisableShadowPropertyReplacementText3", "");
            content = content.Replace("DisableShadowPropertyReplacementText4", "");
            content = content.Replace("DisableShadowPropertyReplacementText5", "");
            content = content.Replace("DisableShadowPropertyReplacementText7", "");
            content = content.Replace(content, DisableShadowPropertyReplacementText6(content));
            return content;
        }
        content = content.Replace(content, DisableShadowPropertyReplacementText1(content));
        content = content.Replace(content, DisableShadowPropertyReplacementText2(content));
        content = content.Replace(content, DisableShadowPropertyReplacementText3(content));
        content = content.Replace(content, DisableShadowPropertyReplacementText4(content));
        content = content.Replace(content, DisableShadowPropertyReplacementText5(content));
        content = content.Replace(content, DisableShadowPropertyReplacementText7(content));
        content = content.Replace("DisableShadowPropertyReplacementText6", "");
        return content;
    }

    public static string DisableShadowPropertyReplacementText1(string content)
    {
        var s = new StringBuilder();
        s.Append("    public string? CreatedByUserId { get; set; }\r\n");
        s.Append("    public string? CreatedByUserName { get; set; }\r\n");
        s.Append("    public DateTime? CreatedDateTime { get; set; }\r\n");
        s.Append("    public string? ModifiedByUserId { get; set; }\r\n");
        s.Append("    public string? ModifiedByUserName { get; set; }\r\n");
        s.Append("    public DateTime? ModifiedDateTime { get; set; }\r\n");
        content = content.Replace("DisableShadowPropertyReplacementText1", s.ToString());
        return content;
    }

    public static string DisableShadowPropertyReplacementText2(string content)
    {
        var s = new StringBuilder();
        s.Append("    public string? CreatedByUserId { get; set; }\r\n");
        s.Append("    public string? CreatedByUserName { get; set; }\r\n");
        s.Append("    public DateTime? CreatedDateTime { get; set; }\r\n");
        s.Append("    public string? ModifiedByUserId { get; set; }\r\n");
        s.Append("    public string? ModifiedByUserName { get; set; }\r\n");
        s.Append("    public DateTime? ModifiedDateTime { get; set; }\r\n");
        content = content.Replace("DisableShadowPropertyReplacementText2", s.ToString());
        return content;
    }

    public static string DisableShadowPropertyReplacementText3(string content)
    {
        var s = new StringBuilder();
        s.Append("    public string? CreatedByUserId { get; set; }\r\n");
        s.Append("    public DateTime? CreatedDateTime { get; set; }\r\n");
        s.Append("    public string? ModifiedByUserId { get; set; }\r\n");
        s.Append("    public DateTime? ModifiedDateTime { get; set; }\r\n");
        content = content.Replace("DisableShadowPropertyReplacementText3", s.ToString());
        return content;
    }

    public static string DisableShadowPropertyReplacementText4(string content)
    {
        var s = new StringBuilder();
        s.Append("            CreatedByUserId = c.CreatedByUserId,\r\n");
        s.Append("            CreatedByUserName = null,\r\n");
        s.Append("            ModifiedByUserId = c.ModifiedByUserId,\r\n");
        s.Append("            ModifiedByUserName = null,\r\n");
        s.Append("            CreatedDateTime = c.CreatedDateTime,\r\n");
        s.Append("            ModifiedDateTime = c.ModifiedDateTime\r\n");
        content = content.Replace("DisableShadowPropertyReplacementText4", s.ToString());
        return content;
    }

    public static string DisableShadowPropertyReplacementText5(string content)
    {
        var s = new StringBuilder();
        s.Append("    public string? CreatedByUserId { get; set; }\r\n");
        s.Append("    public string? CreatedByUserName { get; set; }\r\n");
        s.Append("    public DateTime? CreatedDateTime { get; set; }\r\n");
        s.Append("    public string? ModifiedByUserId { get; set; }\r\n");
        s.Append("    public DateTime? ModifiedDateTime { get; set; }\r\n");
        content = content.Replace("DisableShadowPropertyReplacementText5", s.ToString());
        return content;
    }

    public static string DisableShadowPropertyReplacementText7(string content)
    {
        var s = new StringBuilder();
        s.Append("        query = query.WhereIf(request.CreatedDateTime != null, m => m.CreatedDateTime == request.CreatedDateTime);\r\n");
        s.Append("        query = query.WhereIf(request.ModifiedDateTime != null, m => m.ModifiedDateTime == request.ModifiedDateTime);\r\n");
        s.Append("        query = query.WhereIf(request.CreatedByUserId != null, m => m.CreatedByUserId == request.CreatedByUserId);\r\n");
        s.Append("        query = query.WhereIf(request.ModifiedByUserId != null, m => m.ModifiedByUserId == request.ModifiedByUserId);\r\n");
        content = content.Replace("DisableShadowPropertyReplacementText7", s.ToString());
        return content;
    }

    public static string DisableShadowPropertyReplacementText6(string content)
    {
        var s = new StringBuilder();
        content = content.Replace("DisableShadowPropertyReplacementText6", "        builder.DisableShadowProperty();");
        return content;
    }

    #endregion
}