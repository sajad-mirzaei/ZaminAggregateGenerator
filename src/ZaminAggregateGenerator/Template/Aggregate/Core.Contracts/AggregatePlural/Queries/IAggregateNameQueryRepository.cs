using ZaminAggregateGenerator.Services;

internal class IAggregateNameQueryRepository : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using Zamin.Core.Contracts.Data.Queries;

namespace ProjectName.Core.Contracts.AggregatePlural.Queries;

public interface IAggregateNameQueryRepository : IQueryRepository
{
    Task<AggregateNameByIdDto> SelectAsync(GetAggregateNameByIdQuery request);
    Task<PagedData<AggregateNameDto>> SelectAsync(GetAggregateNameQuery request);
}
";
}