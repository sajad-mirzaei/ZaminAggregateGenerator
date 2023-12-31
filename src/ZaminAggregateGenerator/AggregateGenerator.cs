﻿using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Services;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    internal AggregateGeneratorModel GenModel { get; set; }
    internal List<PropertyModel> PropertyArray { get; set; }
    internal List<string> CsprojFilesList { get; set; }

    public AggregateGenerator(AggregateGeneratorModel genModel)
    {
        GenModel = genModel;
        PropertyArray = Extentoins.ClassParse(GenModel.AggregateClass);
        CsprojFilesList = FileTools.CsprojFilesList(GenModel.ProjectPath);
    }

    public string Generate()
    {
        ResultModel resultModel = this.AggregateGeneratorValidation();
        if (resultModel.Result)
        {
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
        }

        return resultModel.GetString();
    }

    static List<ISourceCode>? GetTemplateLayerFiles(string fileName)
    {
        var layerName = fileName switch
        {
            string s when s.Contains("Core.ApplicationService") => "Core.ApplicationService",
            string s when s.Contains("Core.Contracts") => "Core.Contracts",
            string s when s.Contains("Core.Domain") && !s.ToLower().Contains("domainservice") => "Core.Domain",
            string s when s.Contains("Sql.Commands") => "Sql.Commands",
            string s when s.Contains("Sql.Queries") => "Sql.Queries",
            string s when s.Contains("Endpoints") => "Endpoints",

            //Consider misspellings
            string s when s.Contains("Core.ApplicatoinService") => "Core.ApplicationService",
            string s when s.Contains("Core.ApplicationServices") => "Core.ApplicationService",
            string s when s.Contains("Core.Contract") => "Core.ApplicationService",
            string s when s.Contains("Core.Domains") && !s.ToLower().Contains("domainservice") => "Core.Domain",
            string s when s.Contains("Sql.Command") => "Sql.Commands",
            string s when s.Contains("Sql.Querie") => "Sql.Queries",
            string s when s.Contains("Sql.Query") => "Sql.Queries",
            string s when s.Contains("EndPoints") => "Endpoints",
            string s when s.Contains("EndPoint") => "Endpoints",
            string s when s.Contains("Endpoint") => "Endpoints",

            //ToLower
            string s when s.Contains(("Core.ApplicationService").ToLower()) => "Core.ApplicationService",
            string s when s.Contains(("Core.Contracts").ToLower()) => "Core.Contracts",
            string s when s.Contains(("Core.Domain").ToLower()) && !s.ToLower().Contains("domainservice") => "Core.Domain",
            string s when s.Contains(("Core.Commands").ToLower()) => "Core.Commands",
            string s when s.Contains(("Core.Queries").ToLower()) => "Core.Queries",
            string s when s.Contains(("Core.Endpoints").ToLower()) => "Core.Endpoints",



            _ => null
        };
        return layerName == null ? null : Configs.LayerMappings[layerName];
    }
    void AddDbSetToDbContexts()
    {
        var dbContextFilesList = FileTools.DbContextFilesList(GenModel.ProjectPath);
        GenModel.CommandDbContextPath ??= dbContextFilesList["CommandDbContext"];
        GenModel.QueryDbContextPath ??= dbContextFilesList["QueryDbContext"];

        string content1 = File.ReadAllText(GenModel.CommandDbContextPath, Encoding.Default);
        content1 = content1.Replace("//SqlCommandsCommandDbContextDbSet", "        public DbSet<" + GenModel.AggregateName + "> " + GenModel.AggregatePlural + " { get; set; }\n//SqlCommandsCommandDbContextDbSet");
        content1 = content1.Replace("//SqlCommandsCommandDbContextUsing", "using " + GenModel.ProjectName + ".Core.Domain." + GenModel.AggregatePlural + ".Entities;\n//SqlCommandsCommandDbContextUsing");
        File.WriteAllText(GenModel.CommandDbContextPath, content1, Encoding.Default);

        string content2 = File.ReadAllText(GenModel.QueryDbContextPath, Encoding.Default);
        content2 = content2.Replace("//SqlQueriesQueryDbContextDbSet", "        public virtual DbSet<" + GenModel.AggregateName + "> " + GenModel.AggregatePlural + " { get; set; }\n//SqlQueriesQueryDbContextDbSet");
        File.WriteAllText(GenModel.QueryDbContextPath, content2, Encoding.Default);
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
        AggregatePropertyAdder replacementMethods = new(c, PropertyArray, GenModel);
        return replacementMethods.AddProperties();
    }
    public string GetCorrectClassName(string className)
    {
        var name = className.Contains('_') ? className.Split('_')[0] + ".cs" : className + ".cs";
        return ReplaceAggregateName(name);
    }
}