using ZaminAggregateGenerator.Services;

internal class AggregateNameQueryRepository : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural";
    public string GetSourceCode() => @"using Microsoft.EntityFrameworkCore;
using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateName;
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

    public async Task<AggregateNameByIdDto?> SelectAsync(GetAggregateNameByIdQuery query)
    {
        return await _dbContext.AggregatePlural.Select(c => new AggregateNameByIdDto()
        {
            Id = c.Id,
            BusinessId = c.BusinessId,
SqlQueriesReplacementText1
        }).FirstOrDefaultAsync(c => c.BusinessId.Equals(query.BusinessId));
    }
    public async Task<PagedData<AggregateNameDto>> SelectAsync(GetAggregateNameQuery dto)
    {
        #region Query
        var query = _dbContext.AggregatePlural.AsQueryable();
        #endregion

        #region Filters
SqlQueriesReplacementText2
        query = query.WhereIf(dto.BusinessId != Guid.Empty, m => m.BusinessId == dto.BusinessId);

        query = query.WhereIf(dto.CreatedDateTime != null, m => m.CreatedDateTime == dto.CreatedDateTime);
        query = query.WhereIf(dto.ModifiedDateTime != null, m => m.ModifiedDateTime == dto.ModifiedDateTime);
        query = query.WhereIf(dto.CreatedByUserId != null, m => m.CreatedByUserId == dto.CreatedByUserId);
        query = query.WhereIf(dto.ModifiedByUserId != null, m => m.ModifiedByUserId == dto.ModifiedByUserId);

        #endregion

        #region Result
        PagedData<AggregateNameDto> result = await query.ToPagedData(dto, c => new AggregateNameDto
        {
            Id = c.Id,
            BusinessId = c.BusinessId,
SqlQueriesReplacementText1 ,

            CreatedByUserId = c.CreatedByUserId,
            CreatedByUserName = null,
            ModifiedByUserId = c.ModifiedByUserId,
            ModifiedByUserName = null,
            CreatedDateTime = c.CreatedDateTime,
            ModifiedDateTime = c.ModifiedDateTime
        });

        return result;
        #endregion
    }
}
";
}