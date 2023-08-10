using ZaminAggregateGenerator.TemplateContentChange;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    public void Run()
    {
        var filesList = new FileTools().FilesList(ProjectPath, ".csproj", true, LayersList);

        CopyWithReplacement oMoveTemplate = new()
        {
            AggregatePlural = AggregatePlural,
            AggregateName = AggregateName,
            ProjectName = ProjectName,
        };

        foreach (string file in filesList)
        {
            string directoryPath = Path.GetDirectoryName(file);
            string fileName = Path.GetFileName(file);
            oMoveTemplate.TargetPath = directoryPath;
            switch (fileName)
            {
                case string s when s.Contains(".ApplicationService"):
                    oMoveTemplate.TemplateFolder = "Core.ApplicationService";
                    break;
                case string s when s.Contains(".Contracts"):
                    oMoveTemplate.TemplateFolder = "Core.Contracts";
                    break;
                case string s when s.Contains(".Domain"):
                    oMoveTemplate.TemplateFolder = "Core.Domain";
                    break;
                case string s when s.Contains("Sql.Commands"):
                    oMoveTemplate.TemplateFolder = "Infra.Data.Sql.Commands";
                    break;
                case string s when s.Contains("Sql.Queries"):
                    oMoveTemplate.TemplateFolder = "Infra.Data.Sql.Queries";
                    break;
                case string s when s.Contains("Endpoints"):
                    oMoveTemplate.TemplateFolder = "Endpoints.API";
                    break;
                default:
                    break;
            }
            oMoveTemplate.Exec();
        }
    }
    public string AggregatePlural { get; set; }
    public string AggregateName { get; set; }
    public string ProjectName { get; set; }
    public string ProjectPath { get; set; }

    public List<string> LayersList { get; set; } = new() {
        ".ApplicationService.csproj",
        ".Contracts.csproj",
        ".Domain.csproj",
        ".Sql.Commands.csproj",
        ".Sql.Queries.csproj",
        ".Endpoints.API.csproj"
    };
}

