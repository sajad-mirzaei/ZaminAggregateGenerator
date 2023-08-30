using ZaminAggregateGenerator;

internal class AggregateNameCreated : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Events";
    public string GetSourceCode() => @"using Zamin.Core.Domain.Events;

namespace ProjectName.Core.Domain.AggregatePlural.Events;
public class AggregateNameCreated : IDomainEvent
{
    public Guid BusinessId { get; private set; }
DomainReplacementText1

    public AggregateNameCreated(
        Guid businessId, 
DomainReplacementText2
        )
    {
        BusinessId = businessId;
DomainReplacementText3
    }
}
";
}