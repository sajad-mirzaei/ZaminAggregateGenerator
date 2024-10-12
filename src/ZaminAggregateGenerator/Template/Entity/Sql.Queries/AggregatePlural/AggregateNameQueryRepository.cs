using ZaminAggregateGenerator.Services;

internal class AggregateNameQueryRepository_Entity : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Queries;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNames;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;

namespace Infra.Data.Sql.Queries.AggregatePlural;

public class AggregateNameQueryRepository : BaseQueryRepository<VocQueryDbContext>,
    IAggregateNameQueryRepository
{
    public AggregateNameQueryRepository(VocQueryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AggregateNameByIdDto> SelectAsync(GetAggregateNameByIdQuery request)
    {
        return await _dbContext.AggregatePlural.Select(c => new AggregateNameByIdDto()
        {
            Id = c.Id,
            Subject = c.Subject,
            EntityNameTypeId = c.EntityNameTypeId,
            EntityNamePath = c.EntityNamePath,
            StatusId = c.StatusId,
            PriorityId = c.PriorityId,
            CreatedByUserId = c.CreatedByUserId,
            LastSenderId = c.LastSenderId,
            LastReceiverId = c.LastReceiverId,
            AssignedUserId = c.AssignedUserId,
            Rate = c.Rate,
            IsDeleted = c.IsDeleted,
            CategoryId = c.CategoryId,
            CreatedDate = c.CreatedDate,
            ClosedDate = c.ClosedDate
        }).SingleOrDefaultAsync(c => c.Id.Equals(request.Id));
    }
    public async Task<PagedData<AggregateNameDto>> SelectAsync(GetAggregateNameQuery request)
    {
        #region Query
        var query = _dbContext.AggregatePlural.AsQueryable();
        #endregion

        #region Filters
        query = query.WhereIf(request.Subject != null, p => p.Subject.Contains(request.Subject));
        query = query.WhereIf(request.EntityNameTypeId != null, m => m.EntityNameTypeId == request.EntityNameTypeId);
        query = query.WhereIf(request.EntityNamePath != null, p => p.EntityNamePath.Contains(request.EntityNamePath));
        query = query.WhereIf(request.StatusId != null, m => m.StatusId == request.StatusId);
        query = query.WhereIf(request.PriorityId != null, m => m.PriorityId == request.PriorityId);
        query = query.WhereIf(request.CreatedByUserId != null, m => m.CreatedByUserId == request.CreatedByUserId);
        query = query.WhereIf(request.LastSenderId != null, m => m.LastSenderId == request.LastSenderId);
        query = query.WhereIf(request.LastReceiverId != null, m => m.LastReceiverId == request.LastReceiverId);
        query = query.WhereIf(request.AssignedUserId != null, m => m.AssignedUserId == request.AssignedUserId);
        query = query.WhereIf(request.Rate != null, m => m.Rate == request.Rate);
        query = query.WhereIf(request.IsDeleted != null, m => m.IsDeleted == request.IsDeleted);
        query = query.WhereIf(request.CategoryId != null, m => m.CategoryId == request.CategoryId);
        query = query.WhereIf(request.CreatedDate != null, m => m.CreatedDate == request.CreatedDate);
        query = query.WhereIf(request.ClosedDate != null, m => m.ClosedDate == request.ClosedDate);
        #endregion

        #region Result
        PagedData<AggregateNameDto> result = await query.ToPagedData(request, c => new AggregateNameDto
        {
            Id = c.Id,
            Subject = c.Subject,
            EntityNameTypeId = c.EntityNameTypeId,
            EntityNamePath = c.EntityNamePath,
            StatusId = c.StatusId,
            PriorityId = c.PriorityId,
            CreatedByUserId = c.CreatedByUserId,
            LastSenderId = c.LastSenderId,
            LastReceiverId = c.LastReceiverId,
            AssignedUserId = c.AssignedUserId,
            Rate = c.Rate,
            IsDeleted = c.IsDeleted,
            CategoryId = c.CategoryId,
            CreatedDate = c.CreatedDate,
            ClosedDate = c.ClosedDate
        });

        return result;
        #endregion
    }

    public async Task<EntityNameByIdDto> SelectAsync(GetEntityNameByIdQuery request)
    {
        return await _dbContext.EntityNames.Select(c => new EntityNameByIdDto()
        {
            Id = c.Id,
            AggregateNameId = c.AggregateNameId,
            Message = c.Message,
            CreatedByUserId = c.CreatedByUserId,
            Accessible = c.Accessible,
            CreatedDate = c.CreatedDate
        }).SingleOrDefaultAsync(c => c.Id.Equals(request.Id));
    }
    public async Task<PagedData<EntityNameDto>> SelectAsync(GetEntityNameQuery request)
    {
        #region Query
        var query = _dbContext.EntityNames.AsQueryable();
        #endregion

        #region Filters
        query = query.WhereIf(request.AggregateNameId != null, m => m.AggregateNameId == request.AggregateNameId);
        query = query.WhereIf(request.Message != null, p => p.Message.Contains(request.Message));
        query = query.WhereIf(request.CreatedByUserId != null, m => m.CreatedByUserId == request.CreatedByUserId);
        query = query.WhereIf(request.Accessible != null, m => m.Accessible == request.Accessible);
        query = query.WhereIf(request.CreatedDate != null, m => m.CreatedDate == request.CreatedDate);
        #endregion

        #region Result
        PagedData<EntityNameDto> result = await query.ToPagedData(request, c => new EntityNameDto
        {
            Id = c.Id,
            AggregateNameId = c.AggregateNameId,
            Message = c.Message,
            CreatedByUserId = c.CreatedByUserId,
            Accessible = c.Accessible,
            CreatedDate = c.CreatedDate
        });

        return result;
        #endregion
    }
}
";
}
