namespace ZaminAggregateGenerator.Tools;

internal static class Configs
{
    internal static List<string> CsprojList { get; set; } = new() {
        "ApplicationService.csproj",
        "Contracts.csproj",
        "Domain.csproj",
        "Sql.Commands.csproj",
        "Sql.Queries.csproj",
        "Endpoints.API.csproj"
    };
    internal static Dictionary<string, List<ISourceCode>> LayersList { get; set; } = new()
    {
        {
            "Core.ApplicationService", new List<ISourceCode>
            {
                new CreateAggregateNameCommandHandler(),
                new CreateAggregateNameValidator(),

            }
        }
    };

    internal static string TemplatePath { get; set; } = "\\Template\\";
}
