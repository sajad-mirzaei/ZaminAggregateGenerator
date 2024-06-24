using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameCommand : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Commands\CreateAggregateName";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.ModuleName.AggregatePlural.Commands.CreateAggregateName;

public class CreateAggregateNameCommand
{
AppCodeReplacementText1
}
";
}