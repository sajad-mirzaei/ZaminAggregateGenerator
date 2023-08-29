public class GetAggregateNameQueryValidator : ISourceCode
{
    public string GetSourceCode() => @"using FluentValidation;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Domain.Common;
using Zamin.Extensions.Translations.Abstractions;

namespace ProjectName.Core.ApplicationService.AggregatePlural.Queries.GetAggregatePlural;

public class GetAggregateNameQueryValidator : AbstractValidator<GetAggregateNameQuery>
{
    public GetAggregateNameQueryValidator(ITranslator translator)
    {
        //RuleFor(p => p.FirstName).MinimumLength(2).WithMessage(translator[ResourceKeys.InValidMinLengthError]);
        //RuleFor(p => p.LastName).MinimumLength(2).WithMessage(translator[ResourceKeys.InValidMinLengthError]);
    }
}
";
}