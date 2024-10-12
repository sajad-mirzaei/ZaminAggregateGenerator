using ZaminAggregateGenerator.Services;

internal class AggregateNameQueryRepository : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural";
    public string GetSourceCode() => @"using Microsoft.EntityFrameworkCore;
using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.Infra.Data.Sql.Queries.Common;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Infra.Data.Sql.Queries;
using Zamin.Utilities.Extensions;

namespace ProjectName.Infra.Data.Sql.Queries.AggregatePlural;

public class AggregateNameQueryRepository : BaseQueryRepository<ProjectNameQueryDbContext>,
    IAggregateNameQueryRepository
{
    public AggregateNameQueryRepository(ProjectNameQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AggregateNameByIdDto> SelectAsync(GetAggregateNameByIdQuery request)
    {
        return await _dbContext.AggregatePlural.Select(c => new AggregateNameByIdDto()
        {
            Id = c.Id,
SqlQueriesReplacementText1
        }).SingleOrDefaultAsync(c => c.Id.Equals(request.Id));
    }
    public async Task<PagedData<AggregateNameDto>> SelectAsync(GetAggregateNameQuery request)
    {
        #region Query
        var query = _dbContext.AggregatePlural.AsQueryable();
        #endregion

        #region Filters
SqlQueriesReplacementText2

DisableShadowPropertyReplacementText7

        #endregion

        #region Result
        PagedData<AggregateNameDto> result = await query.ToPagedData(request, c => new AggregateNameDto
        {
            Id = c.Id,
SqlQueriesReplacementText1 ,

DisableShadowPropertyReplacementText4
        });

        return result;
        #endregion
    }
}
";
}