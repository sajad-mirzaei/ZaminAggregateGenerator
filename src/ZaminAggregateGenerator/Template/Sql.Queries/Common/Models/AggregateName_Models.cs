using ZaminAggregateGenerator;

internal class AggregateName_Models : ISourceCode
{
    public string GetClassPath() => @"Common\Models";
    public string GetSourceCode() => @"namespace ProjectName.Infra.Data.Sql.Queries.Common.Models;

public partial class AggregateName
{
    public long Id { get; set; }
   
SqlQueriesReplacementText3

    public string? CreatedByUserId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public Guid BusinessId { get; set; }
}
";
}