using ZaminAggregateGenerator.Services;

internal class AggregateNameCreated : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Events";
    public string GetSourceCode() => @"using Zamin.Core.Domain.Events;

namespace ProjectName.Core.Domain.AggregatePlural.Events;
public class AggregateNameCreated : IDomainEvent
{
    public IdTypeReplacement Id { get; set; }
DomainReplacementTextEvents1

    public AggregateNameCreated(
        IdTypeReplacement id, 
DomainReplacementText2
        )
    {
        Id = id;
DomainReplacementText3
    }
}
";
}