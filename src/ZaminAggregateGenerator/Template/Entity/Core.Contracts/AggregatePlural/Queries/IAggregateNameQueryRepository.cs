using ZaminAggregateGenerator.Services;

internal class IAggregateNameQueryRepository_Entity : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNames;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;

namespace ProjectName.Core.Contracts.AggregatePlural.Queries;

public interface IAggregateNameQueryRepository : IQueryRepository
{
    Task<AggregateNameByIdDto> SelectAsync(GetAggregateNameByIdQuery request);
    Task<PagedData<AggregateNameDto>> SelectAsync(GetAggregateNameQuery request);
    Task<EntityNameByIdDto> SelectAsync(GetEntityNameByIdQuery request);
    Task<PagedData<EntityNameDto>> SelectAsync(GetEntityNameQuery request);
}
";
}