using ZaminAggregateGenerator.Services;

internal class GetEntityNameQueryHandler : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityPlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityPlural;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Queries.GetEntityPlural;

public class GetEntityNameQueryHandler : QueryHandler<GetEntityNameQuery, PagedData<EntityNameDto>>
{
    public IAggregateNameQueryRepository _aggregateNameQueryRepository;
    public GetEntityNameQueryHandler(ZaminServices zaminServices, IAggregateNameQueryRepository aggregateNameQueryRepository) : base(zaminServices)
    {
        _aggregateNameQueryRepository = aggregateNameQueryRepository;
    }

    public override async Task<QueryResult<PagedData<EntityNameDto>>> Handle(GetEntityNameQuery query)
    {
        var aggregatePlural = await _aggregateNameQueryRepository.SelectAsync(query);
        return await ResultAsync(aggregatePlural);
    }
}
";
}