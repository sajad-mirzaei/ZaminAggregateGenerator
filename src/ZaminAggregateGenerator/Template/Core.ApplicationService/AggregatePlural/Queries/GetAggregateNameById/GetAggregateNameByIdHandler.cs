using ZaminAggregateGenerator;

internal class GetAggregateNameByIdHandler : ISourceCode
{
    public string GetClassPath() => @"Core.ApplicationService\AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Utilities;

namespace ProjectName.Core.ApplicationService.AggregatePlural.Queries.GetAggregateNameById;

public class GetAggregateNameByIdHandler : QueryHandler<GetAggregateNameByIdQuery, AggregateNameByIdDto>
{
    private readonly IAggregateNameQueryRepository _aggregateNameQueryRepository;

    public GetAggregateNameByIdHandler(ZaminServices zaminServices,
                                      IAggregateNameQueryRepository aggregateNameQueryRepository) : base(zaminServices)
    {
        _aggregateNameQueryRepository = aggregateNameQueryRepository;
    }

    public override async Task<QueryResult<AggregateNameByIdDto>> Handle(GetAggregateNameByIdQuery query)
    {
        var aggregateName = await _aggregateNameQueryRepository.SelectAsync(query);
        return Result(aggregateName);
    }
}
";
}