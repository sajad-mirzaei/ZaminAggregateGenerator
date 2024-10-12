using ZaminAggregateGenerator.Services;

internal class AggregateNameCommandRepository_Entity : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Domain.AggregatePlural.Entities;
using Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace Infra.Data.Sql.Commands.AggregatePlural;

public class AggregateNameCommandRepository :
        BaseCommandRepository<AggregateName, VocCommandDbContext, long>,
        IAggregateNameCommandRepository
{
    public AggregateNameCommandRepository(VocCommandDbContext dbContext) : base(dbContext)
    {
    }
}
";
}