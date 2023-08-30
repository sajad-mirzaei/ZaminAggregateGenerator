using ZaminAggregateGenerator;

internal class IAggregateNameQueryRepository : ISourceCode
{
    public string GetClassPath() => @"Core.Contracts\AggregatePlural\Queries";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateName;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using Zamin.Core.Contracts.Data.Queries;

namespace ProjectName.Core.Contracts.AggregatePlural.Queries;

public interface IAggregateNameQueryRepository : IQueryRepository
{
    Task<AggregateNameByIdDto?> SelectAsync(GetAggregateNameByIdQuery dto);
    Task<PagedData<AggregateNameDto>> SelectAsync(GetAggregateNameQuery dto);
}
";
}