using ZaminAggregateGenerator;

internal class AggregateNameCommandRepository : ISourceCode
{
    public string GetClassPath() => @"Infra.Data.Sql.Commands\AggregatePlural";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Domain.AggregatePlural.Entities;
using ProjectName.Infra.Data.Sql.Commands.Common;
using Zamin.Infra.Data.Sql.Commands;

namespace ProjectName.Infra.Data.Sql.Commands.AggregatePlural;

public class AggregateNameCommandRepository :
        BaseCommandRepository<AggregateName, ProjectNameCommandDbContext>,
        IAggregateNameCommandRepository
{
    public AggregateNameCommandRepository(ProjectNameCommandDbContext dbContext) : base(dbContext)
    {
    }
}
";
}