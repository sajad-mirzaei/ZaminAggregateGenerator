using ZaminAggregateGenerator.Services;

internal class GetAggregateNameByIdQuery : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.ModuleName.AggregatePlural.Queries.GetAggregateNameById;

public class GetAggregateNameByIdQuery
{
    public int Id { get; set; }
AppCodeReplacementText1
}
";
}