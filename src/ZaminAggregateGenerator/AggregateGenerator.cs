using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    private AggregateGeneratorModel GenModel { get; set; }
    private List<PropertyModel> PropertyArray { get; set; }
    private List<string> CsprojFilesList { get; set; }

    public AggregateGenerator(AggregateGeneratorModel genModel)
    {
        GenModel = genModel;
        PropertyArray = StringExtentoins.ClassParse(GenModel.AggregateClass);
        CsprojFilesList = FileTools.FilesList(GenModel.ProjectPath, ".csproj", true, Configs.TargetCsprojFiles);
    }

    public void Generate()
    {
        foreach (string csprojFilePath in CsprojFilesList)
        {
            var csprojFileDirectoryPath = Path.GetDirectoryName(csprojFilePath) ?? "";
            string csprojFileName = Path.GetFileName(csprojFilePath);
            List<ISourceCode> templateLayerFiles = GetTemplateLayerFiles(csprojFileName);
            foreach (ISourceCode templateLayerFile in templateLayerFiles)
            {
                var classPath = templateLayerFile.GetClassPath();
                var sourceCode = templateLayerFile.GetSourceCode();

                classPath = ReplaceAggregateName(classPath);
                sourceCode = ReplaceAggregateName(sourceCode);

                DirectoryTools.CreatePathDirectories(classPath, csprojFileDirectoryPath);

                var targetDirectoryPath = Path.Combine(csprojFileDirectoryPath, classPath);

                string targetFileName = FileTools.GetCurrectClassName(templateLayerFile.GetType().Name);

                var targetFilePath = Path.Combine(targetDirectoryPath, targetFileName);

                File.WriteAllText(targetFilePath, sourceCode, Encoding.Default);
            }

            /*foreach (string dirPath in Directory.GetDirectories(templatePath, "*", SearchOption.AllDirectories))
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
            }*/
        }
        AddDbSetToDbContexts();
    }

    static List<ISourceCode> GetTemplateLayerFiles(string fileName)
    {
        var layerName = fileName switch
        {
            string s when s.Contains("Core.ApplicationService") => "Core.ApplicationService",
            string s when s.Contains("Core.Contracts") => "Core.Contracts",
            string s when s.Contains("Core.Domain") => "Core.Domain",
            string s when s.Contains("Sql.Commands") => "Sql.Commands",
            string s when s.Contains("Sql.Queries") => "Sql.Queries",
            string s when s.Contains("Endpoints") => "Endpoints",
            _ => "Core.ApplicationService"
        };
        return Configs.LayerMappings[layerName];
    }
    void AddDbSetToDbContexts()
    {
        //CommandDbContext
        var commandDbContextPath = string.IsNullOrWhiteSpace(GenModel.CommandDbContextPath.Trim()) ? GenModel.CommandDbContextPath.Trim() : GenModel.ProjectPath + "\\2.Infra\\Data\\" + GenModel.ProjectName + ".Infra.Data.Sql.Commands\\Common\\" + GenModel.ProjectName + "CommandDbContext.cs";
        var queryDbContextPath = string.IsNullOrWhiteSpace(GenModel.CommandDbContextPath.Trim()) ? GenModel.CommandDbContextPath.Trim() : GenModel.ProjectPath + "\\2.Infra\\Data\\" + GenModel.ProjectName + ".Infra.Data.Sql.Queries\\Common\\" + GenModel.ProjectName + "QueryDbContext.cs";

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
                    .Replace("AggregatePlural", GenModel.AggregatePlural)
                    .Replace("aggregatePlural", GenModel.AggregatePlural.ToLowerFirstChar())
                    .Replace("AggregateName", GenModel.AggregateName)
                    .Replace("aggregateName", GenModel.AggregateName.ToLowerFirstChar())
                    .Replace("ProjectName", GenModel.ProjectName)
                    .Replace("projectName", GenModel.ProjectName.ToLowerFirstChar());
    }
    internal string TemplateContentChange(string c)
    {
        ReplacementMethods replacementMethods = new(c, PropertyArray, GenModel);
        return replacementMethods.Exec();
    }
}