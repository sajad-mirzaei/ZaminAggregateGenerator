using ZaminAggregateGenerator.Services;

internal class AddEntityNameCommandHandler : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\AddEntityName";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Contracts.AggregatePlural.Commands.AddEntityName;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Commands.AddEntityName;

public class AddEntityNameCommandHandler : CommandHandler<AddEntityNameCommand, IdTypeReplacement>
{
    private readonly IAggregateNameCommandRepository _aggregateNameCommandRepository;
    public AddEntityNameCommandHandler(ZaminServices zaminServices, IAggregateNameCommandRepository aggregateNameCommandRepository) : base(zaminServices)
    {
        _aggregateNameCommandRepository = aggregateNameCommandRepository;
    }

    public override async Task<CommandResult<IdTypeReplacement>> Handle(AddEntityNameCommand request)
    {
        var entity = await _aggregateNameCommandRepository.GetGraphAsync(request.AggregateNameId);
        var userId = int.Parse(_zaminServices.UserInfoService.UserId());
        var entityName = entity.AddEntityName(request.Message, userId, request.Accessible);
        await _aggregateNameCommandRepository.CommitAsync();

        return await OkAsync(entityName.Id);
    }
}
";
}