using ZaminAggregateGenerator;

internal class GetAggregateNameByIdQuery : ISourceCode
{
    public string GetClassPath() => @"Core.Contracts\AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"using Zamin.Core.Contracts.ApplicationServices.Queries;

namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;

public class GetAggregateNameByIdQuery : IQuery<AggregateNameByIdDto>
{
    public Guid BusinessId { get; set; }
}
";
}