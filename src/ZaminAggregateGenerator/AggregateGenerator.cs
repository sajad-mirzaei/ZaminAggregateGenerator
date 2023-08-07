namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    List<string> LayersList = new() {
        ".ApplicationService.csproj",
        ".Contracts.csproj",
        ".Domain.csproj",
        ".Sql.Commands.csproj",
        ".Sql.Queries.csproj",
        ".Endpoints.API.csproj"
    };

    public string[] Main()
    {
        string projectPath = "D:\\.NET\\Github\\MyGithub\\voc\\src";
        var filesList = new FileTools().FilesList(projectPath, ".csproj", true, LayersList);

        MoveTemplate oMoveTemplate = new()
        {
            AggregatePlural = "OldTable2s",
            AggregateName = "OldTable2",
            ProjectName = "Voc"
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

            /*string v = fileName switch
            {
                fileName.Contains(".ApplicationService") => "Core.ApplicationService",
                fileName.Contains(".Contracts") => "Core.ApplicationService",
                fileName.Contains(".Domain") => "Core.ApplicationService",
                fileName.Contains("Sql.Commands") => "Core.ApplicationService",
                fileName.Contains("Sql.Queries") => "Core.ApplicationService",
                fileName.Contains(".ApplicationService") => "Core.ApplicationService",

            };
            oMoveTemplate.TemplateFolder = v;*/
            oMoveTemplate.Exex();
        }
        return filesList;
    }
}