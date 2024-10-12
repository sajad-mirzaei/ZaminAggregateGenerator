using ZaminAggregateGenerator.Services;

internal class AggregateName : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Entities";
    public string GetSourceCode() => @"using ProjectName.Core.Domain.AggregatePlural.Events;

namespace ProjectName.Core.Domain.AggregatePlural.Entities;
public class AggregateName : AggregateRoot<long>
{
    #region Properties
    public string Subject { get; private set; }
    public int EntityNameTypeId { get; private set; }
    public string EntityNamePath { get; private set; }
    public int StatusId { get; private set; }
    public int PriorityId { get; private set; }
    public int CreatedByUserId { get; private set; }
    public int LastSenderId { get; private set; }
    public int LastReceiverId { get; private set; }
    public int? AssignedUserId { get; private set; }
    public double Rate { get; private set; }
    public bool IsDeleted { get; private set; }
    public int CategoryId { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime ClosedDate { get; private set; }

    private List<EntityName> _entityNames = new List<EntityName>();
    public IReadOnlyCollection<EntityName> EntityNames => _entityNames.ToList();

    #endregion

    #region Constructors
    private AggregateName()
    {
    }

    private AggregateName(
        string subject,
        int entityNameTypeId,
        string entityNamePath,
        int statusId,
        int priorityId,
        int createdByUserId,
        int lastSenderId,
        int lastReceiverId,
        int? assignedUserId,
        double rate,
        bool isDeleted,
        int categoryId,
        DateTime createdDate,
        DateTime closedDate
    )
    {
        Subject = subject;
        EntityNameTypeId = entityNameTypeId;
        EntityNamePath = entityNamePath;
        StatusId = statusId;
        PriorityId = priorityId;
        CreatedByUserId = createdByUserId;
        LastSenderId = lastSenderId;
        LastReceiverId = lastReceiverId;
        AssignedUserId = assignedUserId;
        Rate = rate;
        IsDeleted = isDeleted;
        CategoryId = categoryId;
        CreatedDate = createdDate;
        ClosedDate = closedDate;

        AddEvent(new AggregateNameCreated(
            Id,
            subject,
            entityNameTypeId,
            entityNamePath,
            statusId,
            priorityId,
            createdByUserId,
            lastSenderId,
            lastReceiverId,
            assignedUserId,
            rate,
            isDeleted,
            categoryId,
            createdDate,
            closedDate
        ));
    }

    public static AggregateName Create(
        string subject,
        int entityNameTypeId,
        string entityNamePath,
        int statusId,
        int priorityId,
        int createdByUserId,
        int lastSenderId,
        int lastReceiverId,
        int? assignedUserId,
        double rate,
        bool isDeleted,
        int categoryId,
        DateTime createdDate,
        DateTime closedDate
    ) => new(
        subject,
        entityNameTypeId,
        entityNamePath,
        statusId,
        priorityId,
        createdByUserId,
        lastSenderId,
        lastReceiverId,
        assignedUserId,
        rate,
        isDeleted,
        categoryId,
        createdDate,
        closedDate
    );
    #endregion

    #region EntityNames
    public EntityName AddEntityName(string message, int createdByUserId, int accessible)
    {
        var entityName = EntityName.Create(message, createdByUserId, accessible);
        _entityNames.Add(entityName);
        AddEvent(new EntityNameAdded(
            entityName.Id,
            Id,
            entityName.Message,
            entityName.CreatedByUserId,
            entityName.Accessible,
            entityName.CreatedDate
        ));

        return entityName;
    }
    #endregion
}
";
}