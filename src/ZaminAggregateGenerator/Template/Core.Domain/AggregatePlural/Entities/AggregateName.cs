﻿using ProjectName.Core.Domain.AggregatePlural.Events;
using Zamin.Core.Domain.Entities;
using Zamin.Core.Domain.ValueObjects;

namespace ProjectName.Core.Domain.AggregatePlural.Entities;
public class AggregateName : AggregateRoot
{
    #region Properties
DomainReplacementText1
    #endregion

    #region Constructors

    private AggregateName()
    {
    }

    private AggregateName(BusinessId businessId, 
DomainReplacementText2
    )
    {
        BusinessId = businessId;
DomainReplacementText3
        new AggregateNameCreated(
            businessId.Value, 
DomainReplacementText4
        );
    }
    #endregion

    #region Commands
    public static AggregateName Create(
        BusinessId businessId, 
DomainReplacementText2
        ) => new(
            businessId, 
DomainReplacementText4
        );
    #endregion
}