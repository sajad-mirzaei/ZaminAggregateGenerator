using RazorAggregateGenerator.Models;
using RazorAggregateGenerator.Services;
using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Services;

namespace RazorAggregateGenerator;

public class RazorAggregateGenerator
{
    internal RazorAggregateGeneratorModel GenModel { get; set; }
    internal List<PropertyModel> PropertyArray { get; set; }
    internal List<string> RootFoldersList { get; set; }

    public RazorAggregateGenerator(RazorAggregateGeneratorModel genModel)
    {
        GenModel = genModel;
        PropertyArray = Extentoins.ClassParse(GenModel.AggregateClass);
        RootFoldersList = FileTools.RootFoldersList(GenModel.ProjectPath);
    }

    public string Generate()
    {
        ResultModel resultModel = this.AggregateGeneratorValidation();
        if (resultModel.Result)
        {
            foreach (string folderPath in RootFoldersList)
            {
                List<ISourceCode> templateFolderFiles = GetTemplateFolderFiles(folderPath);
                if (templateFolderFiles == null) continue;
                foreach (ISourceCode templateFolderFile in templateFolderFiles)
                {
                    var classPath = templateFolderFile.GetClassPath();
                    var sourceCode = templateFolderFile.GetSourceCode();

                    classPath = ReplaceAggregateName(classPath);
                    sourceCode = ReplaceAggregateName(sourceCode);
                    sourceCode = TemplateContentChange(sourceCode);
                    var targetFileName = GetCorrectClassName(templateFolderFile.GetType().Name);

                    DirectoryTools.CreateNestedDirectories(classPath, folderPath);

                    var targetDirectoryPath = Path.Combine(folderPath, classPath);
                    var targetFilePath = Path.Combine(targetDirectoryPath, targetFileName);

                    File.WriteAllText(targetFilePath, sourceCode, Encoding.Default);
                }
            }
        }

        return resultModel.GetString();
    }

    static List<ISourceCode>? GetTemplateFolderFiles(string fileName)
    {
        var layerName = fileName switch
        {
            string s when s.Contains("AppCode") => "AppCode",
            string s when s.Contains("AppCodes") => "AppCode",
            string s when s.Contains("appCode") => "AppCode",
            string s when s.Contains("appCodes") => "AppCode",
            string s when s.Contains("appcode") => "AppCode",

            string s when s.Contains("Pages") => "Pages",
            string s when s.Contains("pages") => "Pages",
            _ => null
        };
        return layerName == null ? null : Configs.LayerMappings[layerName];
    }
    internal string ReplaceAggregateName(string input)
    {
        var projectName = GenModel.ProjectName != null ? GenModel.ProjectName + "." : "";
        return input
            .Replace("ProjectName.", projectName)
            .Replace("ModuleName", GenModel.ModuleName)
            .Replace("AggregatePlural", GenModel.AggregatePlural)
            .Replace("aggregatePlural", GenModel.AggregatePlural)
            .Replace("AggregateName", GenModel.AggregateName)
            .Replace("aggregateName", GenModel.AggregateName)
            .Replace("UiFramework", GenModel.UiFrameworkProjectName);
    }
    internal string TemplateContentChange(string c)
    {
        AggregatePropertyAdder replacementMethods = new(c, PropertyArray, GenModel);
        return replacementMethods.AddProperties();
    }
    public string GetCorrectClassName(string className)
    {
        className = className.Contains("__") ? className.Replace("__", ".") : className + ".cs";
        return ReplaceAggregateName(className);
    }
}