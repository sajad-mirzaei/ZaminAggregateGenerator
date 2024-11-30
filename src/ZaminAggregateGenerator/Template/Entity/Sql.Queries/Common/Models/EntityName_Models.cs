using ZaminAggregateGenerator.Services;

internal class EntityName_Models : ISourceCode
{
    public string GetClassPath() => @"Common\Models";
    public string GetSourceCode() => @"namespace ProjectName.Infra.Data.Sql.Queries.Common.Models;

public partial class EntityName
{
    public IdTypeReplacement Id { get; set; }
   
EntitySqlQueriesReplacementText3

DisableShadowPropertyReplacementText3
}
";
}