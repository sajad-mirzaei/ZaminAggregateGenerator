using ZaminAggregateGenerator.Services;

internal class EntityName : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\EntityName";
    public string GetSourceCode() => @"namespace ProjectName.Core.Domain.AggregatePlural.Entities;
public class EntityName : Entity<long>
{
    #region Properties

    public string Message { get; private set; }
    public int CreatedByUserId { get; private set; }
    public int Accessible { get; private set; }
    public DateTime CreatedDate { get; private set; }

    #endregion

    #region Constructors

    private EntityName()
    {
        CreatedDate = DateTime.Now;
    }

    private EntityName(string message,
        int createdByUserId,
        int accessible) : this()
    {
        Message = message;
        CreatedByUserId = createdByUserId;
        Accessible = accessible;
    }

    internal static EntityName Create(string message,
        int createdByUserId,
        int accessible)
    {
        return new(message, createdByUserId, accessible);
    }

    #endregion
}
";
}
