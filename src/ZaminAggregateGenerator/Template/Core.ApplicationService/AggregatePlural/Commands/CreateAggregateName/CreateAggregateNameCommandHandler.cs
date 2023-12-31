﻿using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameCommandHandler : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\CreateAggregateName";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Contracts.AggregatePlural.Commands.CreateAggregateName;
using ProjectName.Core.Domain.AggregatePlural.Entities;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Utilities;

namespace ProjectName.Core.ApplicationService.AggregatePlural.Commands.CreateAggregateName;
public class CreateAggregateNameCommandHandler : CommandHandler<CreateAggregateNameCommand, Guid>
{
    private readonly IAggregateNameCommandRepository _aggregateNameCommandRepository;
    public CreateAggregateNameCommandHandler(ZaminServices zaminServices, IAggregateNameCommandRepository aggregateNameCommandRepository) : base(zaminServices)
    {
        _aggregateNameCommandRepository = aggregateNameCommandRepository;
    }

    public override async Task<CommandResult<Guid>> Handle(CreateAggregateNameCommand createAggregateNameCommand)
    {
        var businessId = Guid.NewGuid();
        AggregateName aggregateName = AggregateName.Create(
            businessId,
ApplicationServiceReplacementText1
        );
        await _aggregateNameCommandRepository.InsertAsync(aggregateName);
        await _aggregateNameCommandRepository.CommitAsync();
        return await OkAsync(businessId);
    }
}
";
}