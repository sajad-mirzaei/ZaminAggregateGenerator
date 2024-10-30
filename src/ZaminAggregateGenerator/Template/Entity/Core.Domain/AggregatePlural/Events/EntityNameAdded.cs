using ZaminAggregateGenerator.Services;

internal class EntityNameAdded : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Events";
    public string GetSourceCode() => @"namespace ProjectName.Core.Domain.AggregatePlural.Events;

public class EntityNameAdded : IDomainEvent
{
    public IdTypeReplacement Id { get; set; }
EntityDomainReplacementTextEvents1

    public EntityNameAdded(
        IdTypeReplacement id, 
EntityDomainReplacementText2
        )
    {
        Id = id;
EntityDomainReplacementText3
    }
}
";
}