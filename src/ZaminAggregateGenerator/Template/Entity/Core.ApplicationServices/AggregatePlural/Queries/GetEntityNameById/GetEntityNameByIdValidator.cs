using ZaminAggregateGenerator.Services;

internal class GetEntityNameByIdValidator : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetEntityNameById";
    public string GetSourceCode() => @"using FluentValidation;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetEntityNameById;
using Zamin.Extensions.Translations.Abstractions;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Queries.GetEntityNameById;
public class GetEntityNameByIdValidator : AbstractValidator<GetEntityNameByIdQuery>
{
    public GetEntityNameByIdValidator(ITranslator translator)
    {
       /*RuleFor(query => query.Id)
        .NotEmpty().WithMessage(translator[ResourceKeys.MustNotNullError]);*/
    }
}
";
}
