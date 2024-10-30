using ZaminAggregateGenerator.Services;

internal class AggregateName_Entities : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Entities";
    public string GetSourceCode() => @"using ProjectName.Core.Domain.AggregatePlural.Events;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace ProjectName.Core.Domain.AggregatePlural.Entities;
public class AggregateName : AggregateRoot<IdTypeReplacement>
{
    #region Properties
DomainReplacementText1

//EntityPropertiesReplacementText
    #endregion

    #region Constructors

    private AggregateName()
    {
    }

    private AggregateName(
DomainReplacementText2
    )
    {
DomainReplacementText3
        AddEvent(new AggregateNameCreated(
            Id,
DomainReplacementText4
        ));
    }
    #endregion

    #region Commands
    public static AggregateName Create(
DomainReplacementText2
        ) => new(
DomainReplacementText4
        );
    #endregion

//EntityMethodsReplacementText
}
";
}