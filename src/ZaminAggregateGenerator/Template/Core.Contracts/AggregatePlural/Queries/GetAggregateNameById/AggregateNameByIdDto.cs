using ZaminAggregateGenerator.Services;

internal class AggregateNameByIdDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
public class AggregateNameByIdDto
{
    public IdTypeReplacement Id { get; set; }

ContractsReplacementText1

DisableShadowPropertyReplacementText2
}
";
}