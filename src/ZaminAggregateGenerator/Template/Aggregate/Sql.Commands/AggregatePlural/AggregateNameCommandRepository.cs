using ZaminAggregateGenerator.Services;

internal class AggregateNameCommandRepository : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Domain.AggregatePlural.Entities;
using ProjectName.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace ProjectName.Infra.Data.Sql.Commands.AggregatePlural;

public class AggregateNameCommandRepository :
        BaseCommandRepository<AggregateName, ProjectNameCommandDbContext, IdTypeReplacement>,
        IAggregateNameCommandRepository
{
    public AggregateNameCommandRepository(ProjectNameCommandDbContext dbContext) : base(dbContext)
    {
    }
}
";
}