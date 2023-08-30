using ZaminAggregateGenerator;

internal class CreateAggregateNameCommand : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\CreateAggregateName";
    public string GetSourceCode() => @"using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace ProjectName.Core.Contracts.AggregatePlural.Commands.CreateAggregateName;

public class CreateAggregateNameCommand : ICommand<Guid>
{
ContractsReplacementText1
}
";
}