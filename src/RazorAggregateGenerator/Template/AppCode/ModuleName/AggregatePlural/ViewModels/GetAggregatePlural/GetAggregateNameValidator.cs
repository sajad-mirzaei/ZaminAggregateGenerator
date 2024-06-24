using ZaminAggregateGenerator.Services;

internal class GetAggregateNameValidator : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\ViewModels\GetAggregatePlural";
    public string GetSourceCode() => @"using FluentValidation;
using Zamin.Extensions.Translations.Abstractions;

namespace ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.GetAggregatePlural;

public class GetAggregateNameValidator : AbstractValidator<GetAggregateNameViewModel>
{
    public GetAggregateNameValidator(ITranslator translator)
    {

    }
}
";
}