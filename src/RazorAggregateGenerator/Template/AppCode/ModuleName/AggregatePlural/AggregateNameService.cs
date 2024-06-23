using ZaminAggregateGenerator.Services;

internal class AggregateNameService : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural";
    public string GetSourceCode() => @"using ProjectName.AppCode.Compacts.AggregatePlural.Commands.CreateAggregateName;
using ProjectName.AppCode.Compacts.AggregatePlural.Interfaces;
using ProjectName.AppCode.Compacts.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.CreateAggregateName;
using ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.GetAggregatePlural;
using UiFramework.Clients;

namespace ProjectName.AppCode.Compacts.AggregatePlural;

public class AggregateNameService : BaseHttpClient, IAggregateNameService
{
    public AggregateNameService(RazorUIHttpClient client) : base(client.HttpClient)
    {
    }

    public async Task<Result<IEnumerable<AggregateNameByIdDto>>> GetAggregateNameById(GetAggregateNameViewModel viewModel)
    {
        var query = new GetAggregateNameViewModel()
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            DetailId = viewModel.DetailId,
            PictureId = viewModel.PictureId,
            StatusId = viewModel.StatusId
        };

        var url = AggregateNameConfig.GetAggregateNameByIdRoute.ObjectToQueryString(query);
        return await GetAsync<IEnumerable<AggregateNameByIdDto>>(url);
    }

    public async Task<Result> CreateAggregateName(CreateAggregateNameViewModel viewModel)
    {
        var url = AggregateNameConfig.CreateAggregateNameRoute;
        var command = new CreateAggregateNameCommand()
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            DetailId = viewModel.DetailId,
            PictureId = viewModel.PictureId,
            StatusId = viewModel.StatusId
        };
        return await PostAsync<CreateAggregateNameCommand, string>(command, url);
    }
}
";
}