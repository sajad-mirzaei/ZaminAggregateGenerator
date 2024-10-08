using ZaminAggregateGenerator.Services;

internal class AggregateName_Models : ISourceCode
{
    public string GetClassPath() => @"Common\Models";
    public string GetSourceCode() => @"namespace ProjectName.Infra.Data.Sql.Queries.Common.Models;

public partial class AggregateName
{
    public IdTypeReplacement Id { get; set; }
   
SqlQueriesReplacementText3

DisableShadowPropertyReplacementText3
}
";
}