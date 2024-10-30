using ZaminAggregateGenerator.Services;

internal class GetEntityNameByIdQueryHandler : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityNameById";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Queries.GetEntityNameById;

public class GetEntityNameByIdQueryHandler : QueryHandler<GetEntityNameByIdQuery, EntityNameByIdDto>
{
    public IAggregateNameQueryRepository _aggregateNameQueryRepository;

    public GetEntityNameByIdQueryHandler(ZaminServices zaminServices,
        IAggregateNameQueryRepository aggregateNameQueryRepository) : base(zaminServices)
    {
        _aggregateNameQueryRepository = aggregateNameQueryRepository;
    }

    public override async Task<QueryResult<EntityNameByIdDto>> Handle(GetEntityNameByIdQuery query)
    {
        var entityName = await _aggregateNameQueryRepository.SelectAsync(query);
        return await ResultAsync(entityName);
    }
}
";
}