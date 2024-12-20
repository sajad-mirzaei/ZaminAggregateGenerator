﻿using ZaminAggregateGenerator.Services;

internal class AggregateNameController : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural";
    public string GetSourceCode() => @"using Microsoft.AspNetCore.Mvc;
using ProjectName.Core.Contracts.AggregatePlural.Commands.CreateAggregateName;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.EndPoints.Web.Controllers;
//EntityControllerUsingReplacementText

namespace ProjectName.Endpoints.API.AggregatePlural;

[Route(""api/[controller]"")]
public class AggregateNameController : BaseController
{
    [HttpPost(""create"")]
    public async Task<IActionResult> CreateAggregateName([FromBody] CreateAggregateNameCommand createAggregateName)
    {
        return await Create<CreateAggregateNameCommand, IdTypeReplacement>(createAggregateName);
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


//EntityControllerMethodsReplacementText
}
";
}