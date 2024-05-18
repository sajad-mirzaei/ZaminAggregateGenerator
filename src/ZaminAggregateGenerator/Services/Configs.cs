namespace ZaminAggregateGenerator.Services;

internal static class Configs
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
                new AggregateName_Entities(),
                new AggregateNameCreated()
            }
        },
        {
            "Core.Contracts",
            new List<ISourceCode>
            {
                new CreateAggregateNameCommand(),
                new IAggregateNameCommandRepository(),
                new AggregateNameByIdDto(),
                new GetAggregateNameByIdQuery(),
                new AggregateNameDto(),
                new GetAggregateNameQuery(),
                new IAggregateNameQueryRepository(),
                new IAggregateNameService()
            }
        },
        {
            "Core.ApplicationServices",
            new List<ISourceCode>
            {
                new CreateAggregateNameCommandHandler(),
                new CreateAggregateNameValidator(),
                new GetAggregateNameByIdQueryHandler(),
                new GetAggregateNameByIdValidator(),
                new GetAggregateNameQueryHandler(),
                new GetAggregateNameValidator()
            }
        },
        {
            "Sql.Commands",
            new List<ISourceCode>
            {
                new AggregateNameConfig(),
                new AggregateNameCommandRepository()
            }
        },
        {
            "Sql.Queries",
            new List<ISourceCode>
            {
                new AggregateNameQueryRepository(),
                new AggregateName_Models()
            }
        },
        {
            "Endpoints",
            new List<ISourceCode>
            {
                new AggregateNameController()
            }
        }
    };
}
