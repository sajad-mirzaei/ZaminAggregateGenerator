using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameCommand : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Commands\CreateAggregateName";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.Compacts.AggregatePlural.Commands.CreateAggregateName;

public class CreateAggregateNameCommand
{
AppCodeReplacementText1
}
";
}