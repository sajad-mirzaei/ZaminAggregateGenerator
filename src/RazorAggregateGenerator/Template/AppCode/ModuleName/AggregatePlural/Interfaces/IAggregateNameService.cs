using ZaminAggregateGenerator.Services;

internal class IAggregateNameService : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Interfaces";
    public string GetSourceCode() => @"using ProjectName.AppCode.Compacts.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.CreateAggregateName;
using ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.GetAggregatePlural;

namespace ProjectName.AppCode.Compacts.AggregatePlural.Interfaces
{
    public interface IAggregateNameService
    {
        Task<Result<IEnumerable<AggregateNameByIdDto>>> GetAggregateNameById(GetAggregateNameViewModel viewModel);
        Task<Result> CreateAggregateName(CreateAggregateNameViewModel viewModel);
    }
}
";
}