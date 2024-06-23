using ZaminAggregateGenerator.Services;

internal class AggregateNameByIdDto : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.Compacts.AggregatePlural.Queries.GetAggregateNameById;

public class AggregateNameByIdDto
{
    public int Id { get; set; }
AppCodeReplacementText1
}
";
}