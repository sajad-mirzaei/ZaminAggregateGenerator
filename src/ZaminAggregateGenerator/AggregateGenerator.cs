using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Services;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    internal AggregateGeneratorModel GenModel { get; set; }
    internal List<PropertyModel> PropertyArray { get; set; }
    internal List<string> CsprojFilesList { get; set; }
    internal string DbContextPrefix { get; set; }
    internal string CommandDbContextPath { get; set; }
    internal string QueryDbContextPath { get; set; }

    public AggregateGenerator(AggregateGeneratorModel genModel)
    {
        GenModel = genModel;
        PropertyArray = Extensions.ClassParse(GenModel.AggregateClass);
        CsprojFilesList = FileTools.CsprojFilesList(GenModel.ProjectPath);
    }

    public string Generate()
    {
        ResultModel resultModel = this.AggregateGeneratorValidation();
        if (resultModel.Result == false)
            return resultModel.GetString();
        SetDbContexts();
        SetDbContextPrefix();
        foreach (string csprojFilePath in CsprojFilesList)
        {
            var csprojFileDirectoryPath = Path.GetDirectoryName(csprojFilePath) ?? "";
            string csprojFileName = Path.GetFileName(csprojFilePath);
            List<ISourceCode> templateLayerFiles = GetTemplateLayerFiles(csprojFileName);
            if (templateLayerFiles == null) continue;
            foreach (ISourceCode templateLayerFile in templateLayerFiles)
            {
                var classPath = templateLayerFile.GetClassPath();
                var sourceCode = templateLayerFile.GetSourceCode();

                sourceCode = ReplaceDbContextClassNames(sourceCode);
                classPath = ReplaceAggregateName(classPath);
                sourceCode = ReplaceAggregateName(sourceCode);
                sourceCode = TemplateContentChange(sourceCode);
                var targetFileName = GetCorrectClassName(templateLayerFile.GetType().Name);

                DirectoryTools.CreateNestedDirectories(classPath, csprojFileDirectoryPath);

                var targetDirectoryPath = Path.Combine(csprojFileDirectoryPath, classPath);
                var targetFilePath = Path.Combine(targetDirectoryPath, targetFileName);

                File.WriteAllText(targetFilePath, sourceCode, Encoding.Default);
            }
        }
        AddDbSetToDbContexts();
        return resultModel.GetString();
    }

    static List<ISourceCode>? GetTemplateLayerFiles(string fileName)
    {
        var layerName = fileName switch
        {
            string s when s.Contains("Core.ApplicationService") => "Core.ApplicationServices",
            string s when s.Contains("Core.Contracts") => "Core.Contracts",
            string s when s.Contains("Core.Domain") && !s.ToLower().Contains("domainservice") => "Core.Domain",
            string s when s.Contains("Sql.Commands") => "Sql.Commands",
            string s when s.Contains("Sql.Queries") => "Sql.Queries",
            string s when s.Contains("Endpoints") => "Endpoints",

            //Consider misspellings
            string s when s.Contains("Core.ApplicatoinService") => "Core.ApplicationServices",
            string s when s.Contains("Core.ApplicationService") => "Core.ApplicationServices",
            string s when s.Contains("Core.Contract") => "Core.Contracts",
            string s when s.Contains("Core.Domains") && !s.ToLower().Contains("domainservice") => "Core.Domain",
            string s when s.Contains("Sql.Command") => "Sql.Commands",
            string s when s.Contains("Sql.Querie") => "Sql.Queries",
            string s when s.Contains("Sql.Query") => "Sql.Queries",
            string s when s.Contains("EndPoints") => "Endpoints",
            string s when s.Contains("EndPoint") => "Endpoints",
            string s when s.Contains("Endpoint") => "Endpoints",

            //ToLower
            string s when s.Contains(("Core.ApplicationService").ToLower()) => "Core.ApplicationServices",
            string s when s.Contains(("Core.Contracts").ToLower()) => "Core.Contracts",
            string s when s.Contains(("Core.Domain").ToLower()) && !s.ToLower().Contains("domainservice") => "Core.Domain",
            string s when s.Contains(("Sql.Commands").ToLower()) => "Sql.Commands",
            string s when s.Contains(("Sql.Queries").ToLower()) => "Sql.Queries",
            string s when s.Contains(("Endpoints").ToLower()) => "Endpoints",
            _ => null
        };
        return layerName == null ? null : AggregateConfigs.LayerMappings[layerName];
    }
    void AddDbSetToDbContexts()
    {
        GenModel.CommandDbContextPath = CommandDbContextPath;
        GenModel.QueryDbContextPath = QueryDbContextPath;
        var projectName = GenModel.ProjectName != null ? GenModel.ProjectName + "." : "";

        string content1 = File.ReadAllText(GenModel.CommandDbContextPath, Encoding.Default);
        content1 = content1.Replace("//SqlCommandsCommandDbContextDbSet", "        public DbSet<" + GenModel.AggregateName + "> " + GenModel.AggregatePlural + " { get; set; }\r\n//SqlCommandsCommandDbContextDbSet");
        content1 = content1.Replace("//SqlCommandsCommandDbContextUsing", "using " + projectName + "Core.Domain." + GenModel.AggregatePlural + ".Entities;\r\n//SqlCommandsCommandDbContextUsing");
        File.WriteAllText(GenModel.CommandDbContextPath, content1, Encoding.Default);

        string content2 = File.ReadAllText(GenModel.QueryDbContextPath, Encoding.Default);
        content2 = content2.Replace("//SqlQueriesQueryDbContextDbSet", "        public virtual DbSet<" + GenModel.AggregateName + "> " + GenModel.AggregatePlural + " { get; set; }\r\n//SqlQueriesQueryDbContextDbSet");
        File.WriteAllText(GenModel.QueryDbContextPath, content2, Encoding.Default);
    }
    internal string ReplaceAggregateName(string input)
    {
        var projectName = GenModel.ProjectName != null ? GenModel.ProjectName + "." : "";
        input = input.Replace("ProjectName.", projectName);
        var o = input
                    .Replace("IdTypeReplacement", GenModel.IdTypeReplacement.ToString().ToLower())
                    .Replace("AggregatePlural", GenModel.AggregatePlural)
                    .Replace("aggregatePlural", GenModel.AggregatePlural.ToLowerFirstChar())
                    .Replace("AggregateName", GenModel.AggregateName)
                    .Replace("aggregateName", GenModel.AggregateName.ToLowerFirstChar());
        return o;
    }
    internal string TemplateContentChange(string c)
    {
        AggregatePropertyAdder replacementMethods = new(c, PropertyArray, GenModel);
        return replacementMethods.AddProperties();
    }
    public string GetCorrectClassName(string className)
    {
        var name = className.Contains('_') ? className.Split('_')[0] + ".cs" : className + ".cs";
        return ReplaceAggregateName(name);
    }

    public void SetDbContexts()
    {
        var dbContextFilesList = FileTools.DbContextFilesList(GenModel.ProjectPath);
        CommandDbContextPath ??= dbContextFilesList["CommandDbContext"];
        QueryDbContextPath ??= dbContextFilesList["QueryDbContext"];
    }
    public void SetDbContextPrefix()
    {
        var fileName = Path.GetFileNameWithoutExtension(CommandDbContextPath);
        var p = fileName.Replace("CommandDbContext", "");
        DbContextPrefix = p.Replace("QueryDbContext", "");
    }
    public string ReplaceDbContextClassNames(string input)
    {
        input = input.Replace("ProjectNameCommandDbContext", DbContextPrefix + "CommandDbContext");
        input = input.Replace("ProjectNameQueryDbContext", DbContextPrefix + "QueryDbContext");
        return input;
    }
}