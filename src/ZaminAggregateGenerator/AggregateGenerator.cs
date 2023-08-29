using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    private AggregateGeneratorModel GenModel { get; set; }
    private List<PropertyModel> PropertyArray { get; set; }
    private string[] CsprojFilesList { get; set; }

    public AggregateGenerator(AggregateGeneratorModel genModel)
    {
        GenModel = genModel;
        PropertyArray = StringExtentoins.ClassParse(GenModel.AggregateClass);
        CsprojFilesList = FileTools.FilesList(GenModel.ProjectPath, ".csproj", true, Configs.LayersList);
    }

    public void Generate()
    {
        foreach (string file in CsprojFilesList)
        {
            var targetPath = Path.GetDirectoryName(file);

            string fileName = Path.GetFileName(file);

            //var templatePath = Configs.AggregateGeneratorPath + $"\\{Configs.TemplatePath}\\" + GetLayerName(fileName);
            //CopyDirectory(templatePath, targetPath);
        }
        AddDbSetToDbContexts();
    }

    static string GetLayerName(string fileName)
    {
        var layerName = "Core.ApplicationService";
        switch (fileName)
        {
            case string s when s.Contains(".ApplicationService"):
                layerName = "Core.ApplicationService";
                break;
            case string s when s.Contains(".Contracts"):
                layerName = "Core.Contracts";
                break;
            case string s when s.Contains(".Domain"):
                layerName = "Core.Domain";
                break;
            case string s when s.Contains("Sql.Commands"):
                layerName = "Infra.Data.Sql.Commands";
                break;
            case string s when s.Contains("Sql.Queries"):
                layerName = "Infra.Data.Sql.Queries";
                break;
            case string s when s.Contains("Endpoints"):
                layerName = "Endpoints.API";
                break;
            default:
                break;
        }
        return layerName;
    }
    void CopyDirectory(string templatePath, string targetPath)
    {
        try
        {
            foreach (string dirPath in Directory.GetDirectories(templatePath, "*", SearchOption.AllDirectories))
            {
                var newDirPath = ReplaceAggregateName(dirPath);
                Directory.CreateDirectory(newDirPath.Replace(templatePath, targetPath));
            }

            foreach (string sourceFilePath in Directory.GetFiles(templatePath, "*.*", SearchOption.AllDirectories))
            {
                var sourceFilePathReplaced = ReplaceAggregateName(sourceFilePath);
                var destinationFilePath = sourceFilePathReplaced.Replace(templatePath, targetPath);
                destinationFilePath = destinationFilePath.Replace(".csharp", ".cs");
                File.Copy(sourceFilePath, destinationFilePath, true);


                string fileContent = File.ReadAllText(destinationFilePath, Encoding.Default);
                fileContent = ReplaceAggregateName(fileContent);
                fileContent = TemplateContentChange(fileContent);
                string newDestinationFilePath = ReplaceAggregateName(destinationFilePath);
                File.WriteAllText(newDestinationFilePath, fileContent, Encoding.Default);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    void AddDbSetToDbContexts()
    {
        //CommandDbContext
        var commandDbContextPath = GenModel.ProjectPath + "\\2.Infra\\Data\\" + GenModel.ProjectName + ".Infra.Data.Sql.Commands\\Common\\" + GenModel.ProjectName + "CommandDbContext.cs";
        var queryDbContextPath = GenModel.ProjectPath + "\\2.Infra\\Data\\" + GenModel.ProjectName + ".Infra.Data.Sql.Queries\\Common\\" + GenModel.ProjectName + "QueryDbContext.cs";

        string content1 = File.ReadAllText(commandDbContextPath, Encoding.Default);
        content1 = content1.Replace("//SqlCommandsCommandDbContextDbSet", "        public DbSet<" + GenModel.AggregateName + "> " + GenModel.AggregatePlural + " { get; set; }\n//SqlCommandsCommandDbContextDbSet");
        content1 = content1.Replace("//SqlCommandsCommandDbContextUsing", "using " + GenModel.ProjectName + ".Core.Domain." + GenModel.AggregatePlural + ".Entities;\n//SqlCommandsCommandDbContextUsing");
        File.WriteAllText(commandDbContextPath, content1, Encoding.Default);

        string content2 = File.ReadAllText(queryDbContextPath, Encoding.Default);
        content2 = content2.Replace("//SqlQueriesQueryDbContextDbSet", "        public virtual DbSet<" + GenModel.AggregateName + "> " + GenModel.AggregatePlural + " { get; set; }\n//SqlQueriesQueryDbContextDbSet");
        File.WriteAllText(queryDbContextPath, content2, Encoding.Default);
    }
    internal string ReplaceAggregateName(string input)
    {
        return input
                    .Replace("AggregatePlural", GenModel?.AggregatePlural)
                    .Replace("aggregatePlural", GenModel?.AggregatePlural?.ToLowerFirstChar())
                    .Replace("AggregateName", GenModel?.AggregateName)
                    .Replace("aggregateName", GenModel?.AggregateName?.ToLowerFirstChar())
                    .Replace("ProjectName", GenModel?.ProjectName)
                    .Replace("projectName", GenModel?.ProjectName?.ToLowerFirstChar());
    }
    internal string TemplateContentChange(string c)
    {
        ReplacementMethods replacementMethods = new(c, PropertyArray, GenModel);
        return replacementMethods.Exec();
    }
}