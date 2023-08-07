using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateName;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Utilities;

namespace ProjectName.Core.ApplicationService.AggregatePlural.Queries.GetAggregatePlural;

public class GetAggregateNameQueryHandler : QueryHandler<GetAggregateNameQuery, PagedData<AggregateNameDto>>
{
    public IAggregateNameQueryRepository _aggregateNameQueryRepository { get; }
    public GetAggregateNameQueryHandler(ZaminServices zaminServices, IAggregateNameQueryRepository aggregateNameQueryRepository) : base(zaminServices)
    {
        _aggregateNameQueryRepository = aggregateNameQueryRepository;
    }

    public override async Task<QueryResult<PagedData<AggregateNameDto>>> Handle(GetAggregateNameQuery query)
    {
        var aggregatePlural = await _aggregateNameQueryRepository.SelectAsync(query);
        return await ResultAsync(aggregatePlural);
    }
}