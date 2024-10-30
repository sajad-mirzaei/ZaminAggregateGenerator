using ZaminAggregateGenerator.Services;

internal class GetEntityNameQuery : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityPlural";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityPlural;
public class GetEntityNameQuery : PageQuery<PagedData<EntityNameDto>>
{
EntityContractsReplacementTextGetQuery1

DisableShadowPropertyReplacementText5
}
";
}