using ZaminAggregateGenerator.Services;

internal class GetEntityNameValidator : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityPlural";
    public string GetSourceCode() => @"using FluentValidation;
using ProjectName.Core.Domain.Common;
using Zamin.Extensions.Translations.Abstractions;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityPlural;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Queries.GetEntityPlural;

public class GetEntityNameValidator : AbstractValidator<GetEntityNameQuery>
{
    public GetEntityNameValidator(ITranslator translator)
    {
        //RuleFor(p => p.FirstName).MinimumLength(2).WithMessage(translator[ResourceKeys.InValidMinLengthError]);
        //RuleFor(p => p.LastName).MinimumLength(2).WithMessage(translator[ResourceKeys.InValidMinLengthError]);
    }
}
";
}