using ZaminAggregateGenerator.Services;

internal class EntityNameDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityPlural";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNames;
public class EntityNameDto
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