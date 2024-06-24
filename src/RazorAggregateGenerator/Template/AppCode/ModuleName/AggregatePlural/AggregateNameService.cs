using ZaminAggregateGenerator.Services;

internal class AggregateNameService : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural";
    public string GetSourceCode() => @"using ProjectName.AppCode.ModuleName.AggregatePlural.Commands.CreateAggregateName;
using ProjectName.AppCode.ModuleName.AggregatePlural.Interfaces;
using ProjectName.AppCode.ModuleName.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.CreateAggregateName;
using ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.GetAggregatePlural;
using UiFramework.Clients;

namespace ProjectName.AppCode.ModuleName.AggregatePlural;

public class AggregateNameService : BaseHttpClient, IAggregateNameService
{
    public AggregateNameService(RazorUIHttpClient client) : base(client.HttpClient)
    {
    }

    public async Task<Result<IEnumerable<AggregateNameByIdDto>>> GetAggregateNameById(GetAggregateNameViewModel viewModel)
    {
        var query = new GetAggregateNameViewModel()
        {
AppCodeReplacementText2
        };

        var url = AggregateNameConfig.GetAggregateNameByIdRoute.ObjectToQueryString(query);
        return await GetAsync<IEnumerable<AggregateNameByIdDto>>(url);
    }

    public async Task<Result> CreateAggregateName(CreateAggregateNameViewModel viewModel)
    {
        var url = AggregateNameConfig.CreateAggregateNameRoute;
        var command = new CreateAggregateNameCommand()
        {
AppCodeReplacementText2
        };
        return await PostAsync<CreateAggregateNameCommand, string>(command, url);
    }
}
";
}