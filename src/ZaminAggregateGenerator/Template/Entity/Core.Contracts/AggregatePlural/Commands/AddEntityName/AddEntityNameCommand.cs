using ZaminAggregateGenerator.Services;

internal class AddEntityNameCommand : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\AddEntityName";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Commands.AddEntityName;

public class AddEntityNameCommand : ICommand<long>
{
    public int AggregateNameId { get; set; }
    public string Message { get; set; }
    public int CreatedByUserId { get; set; }
    public int Accessible { get; set; }
    public DateTime CreatedDate { get; set; }
}
";
}