using ZaminAggregateGenerator.Services;

internal class GetAggregateNameQuery : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregatePlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateName;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Core.Contracts.Data.Queries;

namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
public class GetAggregateNameQuery : PageQuery<PagedData<AggregateNameDto>>
{
    public long Id { get; set; }
    public Guid BusinessId { get; set; }

ContractsReplacementText1

    public string? CreatedByUserId { get; set; }
    public string? CreatedByUserName { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}
";
}