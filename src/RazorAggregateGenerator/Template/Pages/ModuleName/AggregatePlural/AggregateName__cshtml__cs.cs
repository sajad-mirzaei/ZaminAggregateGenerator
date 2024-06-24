using ZaminAggregateGenerator.Services;

internal class AggregateName__cshtml__cs : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural";
    public string GetSourceCode() => @"using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectName.AppCode.ModuleName.AggregatePlural.Interfaces;
using ProjectName.AppCode.ModuleName.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.CreateAggregateName;
using ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.GetAggregatePlural;
using WWWPGrids;

namespace ProjectName.Pages.ModuleName.AggregatePlural;

public class AggregateNameModel : PageModel
{
    private readonly IAggregateNameService _aggregateNameService;

    [BindProperty]
    public required CreateAggregateNameViewModel AggregateNameViewModel { get; set; }
    private readonly IValidator<CreateAggregateNameViewModel> _aggregateNameViewModelValidator;

    public IEnumerable<AggregateNameByIdDto> AggregateNameByIds { get; set; } = new List<AggregateNameByIdDto>();

    public AggregateNameModel(IAggregateNameService aggregateNameService, IValidator<CreateAggregateNameViewModel> aggregateNameViewModelValidator, CreateAggregateNameViewModel aggregateNameViewModel)
    {
        _aggregateNameService = aggregateNameService;
        _aggregateNameViewModelValidator = aggregateNameViewModelValidator;
        AggregateNameViewModel = aggregateNameViewModel;
    }

    public async Task OnGet()
    {
        var result = await _aggregateNameService.GetAggregateNameById(new GetAggregateNameViewModel());
        if (result.IsSuccess)
            AggregateNameByIds = result.Value;

        SAPGridView oSGV = CreateGrid();
        TempData[""SAPGridView""] = oSGV.GridBind(""MyGrid1"");
    }

    protected SAPGridView CreateGrid()
    {
        SAPGridView oSGV = new();
        oSGV.Grids[""MyGrid1""] = new Grid()
        {
            ContainerId = ""MyGridId"",
            ContainerHeight = 400,
            Data = TestByIds
        };
        return oSGV;
    }

    public async Task<IActionResult> OnPost()
    {
        var validate = await _aggregateNameViewModelValidator.ValidateAsync(AggregateNameViewModel);
        if (validate.IsValid)
        {
            AggregateNameViewModel = new CreateAggregateNameViewModel()
            {
PagesReplacementText3
            };

            var result = await _aggregateNameService.CreateAggregateName(AggregateNameViewModel);
            if (result.IsSuccess)
                return new OkResult();
        }

        return Page();
    }
}
";
}