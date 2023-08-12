using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator.TemplateContentChange;

internal class TemplateCopy
{
    private AggregateGeneratorModel _aggregateGeneratorModel { get; set; }
    private List<PropertyReplacementModel> _propertyArray { get; set; }
    private string[] FilesList { get; set; }
    public TemplateCopy(AggregateGeneratorModel aggregateGeneratorModel)
    {
        _aggregateGeneratorModel = aggregateGeneratorModel;
        _propertyArray = StringExtentoins.ClassParse(_aggregateGeneratorModel.AggregateClass);
        FilesList = new FileTools().FilesList(aggregateGeneratorModel.ProjectPath, ".csproj", true, Configs.LayersList);
    }

    public void PerformCopy()
    {
        foreach (string file in FilesList)
        {
            var targetPath = Path.GetDirectoryName(file);
            var templateFolder = "Core.ApplicationService";
            string fileName = Path.GetFileName(file);
            switch (fileName)
            {
                case string s when s.Contains(".ApplicationService"):
                    templateFolder = "Core.ApplicationService";
                    break;
                case string s when s.Contains(".Contracts"):
                    templateFolder = "Core.Contracts";
                    break;
                case string s when s.Contains(".Domain"):
                    templateFolder = "Core.Domain";
                    break;
                case string s when s.Contains("Sql.Commands"):
                    templateFolder = "Infra.Data.Sql.Commands";
                    break;
                case string s when s.Contains("Sql.Queries"):
                    templateFolder = "Infra.Data.Sql.Queries";
                    break;
                case string s when s.Contains("Endpoints"):
                    templateFolder = "Endpoints.API";
                    break;
                default:
                    break;
            }
            var templatePath = Configs.AggregateGeneratorPath + $"\\{Configs.TemplatePath}\\" + templateFolder;
            Exec(templatePath, targetPath);
        }
    }

    public void Exec(string templatePath, string targetPath)
    {
        try
        {
            CopyDirectory(templatePath, targetPath);
            ReplaceTextInDirectory(targetPath);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    void CopyDirectory(string templatePath, string targetPath)
    {
        foreach (string dirPath in Directory.GetDirectories(templatePath, "*", SearchOption.AllDirectories))
        {
            var newDirPath = ReplaceAggregateName(dirPath);
            Directory.CreateDirectory(newDirPath.Replace(templatePath, targetPath));
        }

        foreach (string sourceFilePath in Directory.GetFiles(templatePath, "*.*", SearchOption.AllDirectories))
        {
            var sourceFilePathReplaced = ReplaceAggregateName(sourceFilePath);
            var destFileName = sourceFilePathReplaced.Replace(templatePath, targetPath);
            destFileName = destFileName.Replace(".csharp", ".cs");
            File.Copy(sourceFilePath, destFileName, true);
        }
    }
    void ReplaceTextInDirectory(string targetPath)
    {
        var newTargetPath = targetPath + "\\" + _aggregateGeneratorModel.AggregatePlural;
        foreach (string file in Directory.GetFiles(newTargetPath, "*.*", SearchOption.AllDirectories))
        {
            string content = File.ReadAllText(file, Encoding.UTF8);

            content = ReplaceAggregateName(content);
            content = TemplateContentChange(content);
            string newFile = ReplaceAggregateName(file);

            File.WriteAllText(newFile, content, Encoding.UTF8);
        }
    }
    internal string ReplaceAggregateName(string input)
    {
        return input
                    .Replace("AggregatePlural", _aggregateGeneratorModel.AggregatePlural)
                    .Replace("aggregatePlural", _aggregateGeneratorModel.AggregatePlural.ToLowerFirstChar())
                    .Replace("AggregateName", _aggregateGeneratorModel.AggregateName)
                    .Replace("aggregateName", _aggregateGeneratorModel.AggregateName.ToLowerFirstChar())
                    .Replace("ProjectName", _aggregateGeneratorModel.ProjectName)
                    .Replace("projectName", _aggregateGeneratorModel.ProjectName.ToLowerFirstChar());
    }
    internal string TemplateContentChange(string c)
    {
        c = new ApplicationService(c, _propertyArray, _aggregateGeneratorModel).Invoke();
        c = new Contracts(c, _propertyArray, _aggregateGeneratorModel).Invoke();
        c = new Domain(c, _propertyArray, _aggregateGeneratorModel).Invoke();
        c = new SqlQueries(c, _propertyArray, _aggregateGeneratorModel).Invoke();
        return c;
    }

    /*public string GetTemplateFilePath()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resourceName = "Template";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new ArgumentException("Template file not found: ");
            }

            string tempPath = Path.Combine(Path.GetTempPath(), "");
            using (FileStream fileStream = File.Create(tempPath))
            {
                stream.CopyTo(fileStream);
            }

            return tempPath;
        }
    }
    public string GetProjectDirectoryPath()
    {
        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        UriBuilder uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
    }*/
}
