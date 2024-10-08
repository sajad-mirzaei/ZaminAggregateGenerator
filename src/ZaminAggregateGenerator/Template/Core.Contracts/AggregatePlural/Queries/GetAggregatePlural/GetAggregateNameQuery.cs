using ZaminAggregateGenerator.Services;

internal class GetAggregateNameQuery : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregatePlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Core.Contracts.Data.Queries;

namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
public class GetAggregateNameQuery : PageQuery<PagedData<AggregateNameDto>>
{
ContractsReplacementTextGetQuery1

DisableShadowPropertyReplacementText5
}
";
}