using ZaminAggregateGenerator.Services;

internal class EntityName_Entities : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Entities";
    public string GetSourceCode() => @"namespace ProjectName.Core.Domain.AggregatePlural.Entities;
public class EntityName : Entity<IdTypeReplacement>
{
    #region Properties

EntityDomainReplacementText1

    #endregion

    #region Constructors

    private EntityName()
    {
    }

    private EntityName(
EntityDomainReplacementText2
    )
    {
EntityDomainReplacementText3
    }

    internal static EntityName Create(
EntityDomainReplacementText2
    )
    {
        return new(
EntityDomainReplacementText4
        );
    }

    #endregion
}
";
}
