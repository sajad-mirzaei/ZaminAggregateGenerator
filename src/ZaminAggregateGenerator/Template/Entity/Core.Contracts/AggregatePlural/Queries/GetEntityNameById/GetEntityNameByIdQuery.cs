using ZaminAggregateGenerator.Services;

internal class GetEntityNameByIdQuery : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityNameById";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;

public class GetEntityNameByIdQuery : IQuery<EntityNameByIdDto>
{
    public IdTypeReplacement Id { get; set; }
}
";
}