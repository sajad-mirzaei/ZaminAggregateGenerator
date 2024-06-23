using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameCommand : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Commands\CreateAggregateName";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.Compacts.AggregatePlural.Commands.CreateAggregateName;

public class CreateAggregateNameCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? DetailId { get; set; }
    public byte StatusId { get; set; }
    public int? PictureId { get; set; }
}
";
}