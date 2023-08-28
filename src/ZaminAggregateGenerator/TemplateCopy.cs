﻿using System.Text;
using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator;

internal class TemplateCopy
{
    private AggregateGeneratorModel _aggregateGeneratorModel { get; set; }
    private List<PropertyModel> _propertyArray { get; set; }
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
        AddDbSetToDbContexts();
    }

    public void Exec(string templatePath, string targetPath)
    {
        try
        {
            CopyDirectory(templatePath, targetPath);
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
    void AddDbSetToDbContexts()
    {
        //CommandDbContext
        var commandDbContextPath = _aggregateGeneratorModel.ProjectPath + "\\2.Infra\\Data\\" + _aggregateGeneratorModel.ProjectName + ".Infra.Data.Sql.Commands\\Common\\" + _aggregateGeneratorModel.ProjectName + "CommandDbContext.cs";
        var queryDbContextPath = _aggregateGeneratorModel.ProjectPath + "\\2.Infra\\Data\\" + _aggregateGeneratorModel.ProjectName + ".Infra.Data.Sql.Queries\\Common\\" + _aggregateGeneratorModel.ProjectName + "QueryDbContext.cs";

        string content1 = File.ReadAllText(commandDbContextPath, Encoding.Default);
        content1 = content1.Replace("//SqlCommandsCommandDbContextDbSet", "        public DbSet<" + _aggregateGeneratorModel.AggregateName + "> " + _aggregateGeneratorModel.AggregatePlural + " { get; set; }\n//SqlCommandsCommandDbContextDbSet");
        content1 = content1.Replace("//SqlCommandsCommandDbContextUsing", "using " + _aggregateGeneratorModel.ProjectName + ".Core.Domain." + _aggregateGeneratorModel.AggregatePlural + ".Entities;\n//SqlCommandsCommandDbContextUsing");
        File.WriteAllText(commandDbContextPath, content1, Encoding.Default);

        string content2 = File.ReadAllText(queryDbContextPath, Encoding.Default);
        content2 = content2.Replace("//SqlQueriesQueryDbContextDbSet", "        public virtual DbSet<" + _aggregateGeneratorModel.AggregateName + "> " + _aggregateGeneratorModel.AggregatePlural + " { get; set; }\n//SqlQueriesQueryDbContextDbSet");
        File.WriteAllText(queryDbContextPath, content2, Encoding.Default);
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
        ReplacementMethods replacementMethods = new(c, _propertyArray, _aggregateGeneratorModel);
        return replacementMethods.Exec();
    }
}