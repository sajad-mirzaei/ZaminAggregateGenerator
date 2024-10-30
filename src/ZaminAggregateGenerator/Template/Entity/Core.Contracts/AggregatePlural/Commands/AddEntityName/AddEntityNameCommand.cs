using ZaminAggregateGenerator.Services;

internal class AddEntityNameCommand : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\AddEntityName";
    public string GetSourceCode() => @"namespace ProjectName.Core.Contracts.AggregatePlural.Commands.AddEntityName;

public class AddEntityNameCommand : ICommand<IdTypeReplacement>
{
EntityContractsReplacementText1
}
";
}