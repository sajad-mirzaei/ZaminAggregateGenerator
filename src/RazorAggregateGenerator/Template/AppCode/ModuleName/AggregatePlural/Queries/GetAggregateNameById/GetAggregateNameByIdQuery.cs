using ZaminAggregateGenerator.Services;

internal class GetAggregateNameByIdQuery : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.Compacts.AggregatePlural.Queries.GetAggregateNameById;

public class GetAggregateNameByIdQuery
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? DetailId { get; set; }
    public byte StatusId { get; set; }
    public int? PictureId { get; set; }
}
";
}