namespace ZaminAggregateGenerator.Services;

internal static class EntityConfigs
{
    /*internal static List<string> TargetCsprojFiles { get; set; } = new() {
        "ApplicationServices.csproj",
        "Contracts.csproj",
        "Domain.csproj",
        "Sql.Commands.csproj",
        "Sql.Queries.csproj",
        "Endpoints.API.csproj"
    };*/
    internal static Dictionary<string, List<ISourceCode>> LayerMappings { get; set; } = new()
    {
        {
            "Core.Domain",
            new List<ISourceCode>
            {
                new EntityName_Entities(),
                new EntityNameAdded()
            }
        },
        {
            "Core.Contracts",
            new List<ISourceCode>
            {
                new AddEntityNameCommand(),
                new EntityNameByIdDto(),
                new GetEntityNameByIdQuery(),
                new EntityNameDto(),
                new GetEntityNameQuery()
            }
        },
        {
            "Core.ApplicationServices",
            new List<ISourceCode>
            {
                new AddEntityNameCommandHandler(),
                new AddEntityNameValidator(),
                new GetEntityNameByIdQueryHandler(),
                new GetEntityNameByIdValidator(),
                new GetEntityNameQueryHandler(),
                new GetEntityNameValidator()
            }
        },
        {
            "Sql.Commands",
            new List<ISourceCode>
            {
                new EntityNameConfig()
            }
        },
        {
            "Sql.Queries",
            new List<ISourceCode>
            {
                new EntityName_Models()
            }
        },
        {
            "Endpoints",
            new List<ISourceCode>
            {
            }
        }
    };
}
