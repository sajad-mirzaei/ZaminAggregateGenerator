using ZaminAggregateGenerator.Services;

internal class EntityNameAdded : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Events";
    public string GetSourceCode() => @"namespace ProjectName.Core.Domain.AggregatePlural.Events;

public class EntityNameAdded : IDomainEvent
{
    public long Id { get; set; }
    public long AggregateNameId { get; set; }
    public string Message { get; set; }
    public int CreatedByUserId { get; set; }
    public int Accessible { get; set; }
    public DateTime CreatedDate { get; set; }


    public EntityNameAdded(
        long id,
        long aggregateNameId,
        string message,
        int createdByUserId,
        int accessible,
        DateTime createdDate
        )
    {
        Id = id;
        AggregateNameId = aggregateNameId;
        Message = message;
        CreatedByUserId = createdByUserId;
        Accessible = accessible;
        CreatedDate = createdDate;

    }
}
";
}

