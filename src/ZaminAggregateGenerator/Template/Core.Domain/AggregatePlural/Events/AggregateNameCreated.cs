using ZaminAggregateGenerator.Services;

internal class AggregateNameCreated : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Events";
    public string GetSourceCode() => @"using Zamin.Core.Domain.Events;

namespace ProjectName.Core.Domain.AggregatePlural.Events;
public class AggregateNameCreated : IDomainEvent
{
    public int Id { get; private set; }
DomainReplacementText1

    public AggregateNameCreated(
        int id, 
DomainReplacementText2
        )
    {
        Id = id;
DomainReplacementText3
    }
}
";
}