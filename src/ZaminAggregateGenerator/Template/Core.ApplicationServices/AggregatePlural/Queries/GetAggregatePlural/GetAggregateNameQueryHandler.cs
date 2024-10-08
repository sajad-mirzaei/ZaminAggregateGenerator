﻿using ZaminAggregateGenerator.Services;

internal class GetAggregateNameQueryHandler : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregatePlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Utilities;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Queries.GetAggregatePlural;

public class GetAggregateNameQueryHandler : QueryHandler<GetAggregateNameQuery, PagedData<AggregateNameDto>>
{
    public IAggregateNameQueryRepository _aggregateNameQueryRepository;
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
";
}