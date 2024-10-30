using ZaminAggregateGenerator.Services;

internal class EntityNameByIdDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityNameById";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
public class EntityNameByIdDto
{
    public IdTypeReplacement Id { get; set; }

EntityContractsReplacementText1

DisableShadowPropertyReplacementText2
}
";
}