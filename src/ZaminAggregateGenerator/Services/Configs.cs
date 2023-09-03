namespace ZaminAggregateGenerator.Services;

internal static class Configs
{
    internal static List<string> TargetCsprojFiles { get; set; } = new() {
        "ApplicationService.csproj",
        "Contracts.csproj",
        "Domain.csproj",
        "Sql.Commands.csproj",
        "Sql.Queries.csproj",
        "Endpoints.API.csproj"
    };
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
                new IAggregateNameDomainService()
            }
        },
        {
            "Core.ApplicationService",
            new List<ISourceCode>
            {
                new CreateAggregateNameCommandHandler(),
                new CreateAggregateNameValidator(),
                new GetAggregateNameByIdHandler(),
                new GetAggregateNameByIdValidator(),
                new GetAggregateNameQueryHandler(),
                new GetAggregateNameQueryValidator()
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
