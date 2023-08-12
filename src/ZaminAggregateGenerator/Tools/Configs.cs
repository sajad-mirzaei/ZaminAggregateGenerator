namespace ZaminAggregateGenerator.Tools;

internal static class Configs
{
    internal static List<string> LayersList { get; set; } = new() {
        ".ApplicationService.csproj",
        ".Contracts.csproj",
        ".Domain.csproj",
        ".Sql.Commands.csproj",
        ".Sql.Queries.csproj",
        ".Endpoints.API.csproj"
    };

    internal static string TemplatePath { get; set; } = "\\Template\\";
    internal static string? AggregateGeneratorPath { get; set; }
}
