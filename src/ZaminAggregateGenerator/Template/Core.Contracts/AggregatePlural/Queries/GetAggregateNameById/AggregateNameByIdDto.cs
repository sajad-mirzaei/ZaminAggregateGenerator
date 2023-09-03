using ZaminAggregateGenerator.Services;

internal class AggregateNameByIdDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
public class AggregateNameByIdDto
{
    public long Id { get; set; }
    public Guid BusinessId { get; set; }

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