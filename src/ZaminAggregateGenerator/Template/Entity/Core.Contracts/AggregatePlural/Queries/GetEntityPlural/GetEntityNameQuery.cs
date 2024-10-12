using ZaminAggregateGenerator.Services;

internal class GetEntityNameQuery : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityPlural";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNames;
public class GetEntityNameQuery : PageQuery<PagedData<EntityNameDto>>
{
    public int? AggregateNameId { get; set; }
    public string Message { get; set; }
    public int? CreatedByUserId { get; set; }
    public int? Accessible { get; set; }
    public DateTime? CreatedDate { get; set; }
}
";
}