using ZaminAggregateGenerator.Services;

internal class AggregateNameController_Entity : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Events";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Commands.AddEntityName;
using ProjectName.Core.Contracts.AggregatePlural.Commands.CreateAggregateName;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNames;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;

namespace Endpoints.API.AggregatePlural;

[Route(""api/[controller]"")]
public class AggregateNameController : BaseController
{
    #region AggregatePlural
    [HttpPost(""create"")]
    public async Task<IActionResult> CreateAggregateName([FromBody] CreateAggregateNameCommand createAggregateName)
    {
        return await Create<CreateAggregateNameCommand, long>(createAggregateName);
    }

    [HttpGet(""get"")]
    public async Task<IActionResult> GetAggregateName([FromQuery] GetAggregateNameQuery query)
    {
        return await Query<GetAggregateNameQuery, PagedData<AggregateNameDto>>(query);
    }

    [HttpGet(""getById"")]
    public async Task<IActionResult> GetAggregateNameById([FromQuery] GetAggregateNameByIdQuery query)
    {
        return await Query<GetAggregateNameByIdQuery, AggregateNameByIdDto>(query);
    }
    #endregion

    #region EntityNames
    [HttpPost(""createEntityName"")]
    public async Task<IActionResult> CreateEntityName([FromBody] AddEntityNameCommand createEntityName)
    {
        return await Create<AddEntityNameCommand, long>(createEntityName);
    }

    [HttpGet(""getEntityNames"")]
    public async Task<IActionResult> GetEntityName([FromQuery] GetEntityNameQuery query)
    {
        return await Query<GetEntityNameQuery, PagedData<EntityNameDto>>(query);
    }

    [HttpGet(""getEntityNameById"")]
    public async Task<IActionResult> GetEntityNameById([FromQuery] GetEntityNameByIdQuery query)
    {
        return await Query<GetEntityNameByIdQuery, EntityNameByIdDto>(query);
    }
    #endregion
}
";
}