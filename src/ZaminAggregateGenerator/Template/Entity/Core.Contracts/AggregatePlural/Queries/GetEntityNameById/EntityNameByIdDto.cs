using ZaminAggregateGenerator.Services;

internal class EntityNameByIdDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityNameById";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
public class EntityNameByIdDto
{
    public long Id { get; set; }

    public int AggregateNameId { get; set; }
    public string Message { get; set; }
    public int CreatedByUserId { get; set; }
    public int Accessible { get; set; }
    public DateTime CreatedDate { get; set; }
}
";
}