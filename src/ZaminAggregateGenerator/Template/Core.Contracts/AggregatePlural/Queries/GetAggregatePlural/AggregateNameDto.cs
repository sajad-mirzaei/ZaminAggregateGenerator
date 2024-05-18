using ZaminAggregateGenerator.Services;

internal class AggregateNameDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregatePlural";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
public class AggregateNameDto
{
    public int Id { get; set; }

ContractsReplacementText1

    public string? CreatedByUserId { get; set; }
    public string? CreatedByUserName { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? ModifiedByUserId { get; set; }
    public string? ModifiedByUserName { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}
";
}