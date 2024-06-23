using ZaminAggregateGenerator.Services;

internal class AggregateName__cshtml__cs : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural";
    public string GetSourceCode() => @"using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectName.AppCode.Compacts.AggregatePlural.Interfaces;
using ProjectName.AppCode.Compacts.AggregatePlural.Queries.GetAggregateNameById;
using ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.CreateAggregateName;
using ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.GetAggregatePlural;

namespace ProjectName.Pages.Compacts.AggregatePlural;

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
    }

    public async Task<IActionResult> OnPost()
    {
        var validate = await _aggregateNameViewModelValidator.ValidateAsync(AggregateNameViewModel);
        if (validate.IsValid)
        {
            AggregateNameViewModel = new CreateAggregateNameViewModel()
            {
                FirstName = ""Ali"",
                LastName = ""Reza"",
                PictureId = 1,
                DetailId = 1,
                StatusId = 1
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