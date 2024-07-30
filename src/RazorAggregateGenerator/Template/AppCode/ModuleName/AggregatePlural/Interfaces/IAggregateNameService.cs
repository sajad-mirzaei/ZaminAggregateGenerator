using ZaminAggregateGenerator.Services;

internal class IAggregateNameService : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\Interfaces";
    public string GetSourceCode() => @"using ProjectName.AppCode.ModuleName.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.CreateAggregateName;
using ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.GetAggregatePlural;

namespace ProjectName.AppCode.ModuleName.AggregatePlural.Interfaces
{
    public interface IAggregateNameService
    {
        Task<Result<IEnumerable<AggregateNameByIdDto>>> GetAggregateNameById(GetAggregateNameViewModel viewModel);
        Task<Result> CreateAggregateName(CreateAggregateNameViewModel viewModel);
    }
}";
}