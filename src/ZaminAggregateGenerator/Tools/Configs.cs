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
    internal static Dictionary<string, string> TemplateFilesList { get; set; } = new() {
        { "Core.Domain",  @"\AggregatePlural\Entities\AggregateName.cs" },
        { "Core.Domain", @"\AggregatePlural\Events\AggregateNameCreated.cs" },

        { "", @"Core.ApplicationService\AggregatePlural\Commands\CreateAggregateName\CreateAggregateNameCommandHandler.cs" },
        { "", @"Core.ApplicationService\AggregatePlural\Commands\CreateAggregateName\CreateAggregateNameValidator.cs" },
        { "", @"Core.ApplicationService\AggregatePlural\Queries\GetAggregateNameById\GetAggregateNameByIdHandler.cs" },
        { "", @"Core.ApplicationService\AggregatePlural\Queries\GetAggregateNameById\GetAggregateNameByIdValidator.cs" },
        { "", @"Core.ApplicationService\AggregatePlural\Queries\GetAggregatePlural\GetAggregateNameQueryHandler.cs" },
        { "", @"Core.ApplicationService\AggregatePlural\Queries\GetAggregatePlural\GetAggregateNameQueryValidator.cs" },

        { "", @"Core.Contracts\AggregatePlural\Commands\CreateAggregateName\CreateAggregateNameCommand.cs" },
        { "", @"Core.Contracts\AggregatePlural\Commands\IAggregateNameCommandRepository.cs" },
        { "", @"Core.Contracts\AggregatePlural\Queries\GetAggregateNameById\AggregateNameByIdDto.cs" },
        { "", @"Core.Contracts\AggregatePlural\Queries\GetAggregateNameById\GetAggregateNameByIdQuery.cs" },
        { "", @"Core.Contracts\AggregatePlural\Queries\GetAggregatePlural\AggregateNameDto.cs" },
        { "", @"Core.Contracts\AggregatePlural\Queries\GetAggregatePlural\GetAggregateNameQuery.cs" },
        { "", @"Core.Contracts\AggregatePlural\Queries\IAggregateNameQueryRepository.cs" },
        { "", @"Core.Contracts\AggregatePlural\IAggregateNameDomainService.cs" },

        { "", @"Infra.Data.Sql.Commands\AggregatePlural\Configs\AggregateNameConfig.cs" },
        { "", @"Infra.Data.Sql.Commands\AggregatePlural\AggregateNameCommandRepository.cs" },
        { "", @"Infra.Data.Sql.Queries\AggregatePlural\AggregateNameQueryRepository.cs" },
        { "", @"Infra.Data.Sql.Queries\Common\Models\AggregateName.cs" },

        { "", @"Endpoints.API\AggregatePlural\AggregateNameController.cs" }
    };

    internal static string TemplatePath { get; set; } = "\\Template\\";
}
