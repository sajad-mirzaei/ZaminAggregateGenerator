public class IAggregateNameCommandRepository : ISourceCode
{
    public string GetSourceCode() => @"using ProjectName.Core.Domain.AggregatePlural.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace ProjectName.Core.Contracts.AggregatePlural.Commands;

public interface IAggregateNameCommandRepository : ICommandRepository<AggregateName>
{
}
";
}