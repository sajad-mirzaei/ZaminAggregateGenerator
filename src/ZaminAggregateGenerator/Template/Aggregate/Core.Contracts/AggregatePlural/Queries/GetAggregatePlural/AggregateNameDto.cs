using ZaminAggregateGenerator.Services;

internal class AggregateNameDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregatePlural";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
public class AggregateNameDto
{
    public IdTypeReplacement Id { get; set; }

ContractsReplacementText1

DisableShadowPropertyReplacementText1
}
";
}