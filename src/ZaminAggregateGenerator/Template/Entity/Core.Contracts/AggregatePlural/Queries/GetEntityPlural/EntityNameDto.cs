using ZaminAggregateGenerator.Services;

internal class EntityNameDto : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityPlural";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityPlural;
public class EntityNameDto
{
    public IdTypeReplacement Id { get; set; }

EntityContractsReplacementText1

DisableShadowPropertyReplacementText1
}
";
}