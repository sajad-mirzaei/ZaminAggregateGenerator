﻿using ZaminAggregateGenerator.Services;

internal class GetAggregateNameByIdValidator : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregateNameById";
    public string GetSourceCode() => @"using FluentValidation;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregateNameById;
using Zamin.Extensions.Translations.Abstractions;

namespace ProjectName.Core.ApplicationService.AggregatePlural.Queries.GetAggregateNameById;
public class GetAggregateNameByIdValidator : AbstractValidator<GetAggregateNameByIdQuery>
{
    public GetAggregateNameByIdValidator(ITranslator translator)
    {
       /*RuleFor(query => query.BusinessId)
        .NotEmpty().WithMessage(translator[ResourceKeys.MustNotNullError]);*/
    }
}
";
}