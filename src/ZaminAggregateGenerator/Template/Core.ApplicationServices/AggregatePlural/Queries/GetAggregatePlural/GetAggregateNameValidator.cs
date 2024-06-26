﻿using ZaminAggregateGenerator.Services;

internal class GetAggregateNameValidator : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Queries\GetAggregatePlural";
    public string GetSourceCode() => @"using FluentValidation;
using ProjectName.Core.Contracts.AggregatePlural.Queries.GetAggregatePlural;
using ProjectName.Core.Domain.Common;
using Zamin.Extensions.Translations.Abstractions;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Queries.GetAggregatePlural;

public class GetAggregateNameValidator : AbstractValidator<GetAggregateNameQuery>
{
    public GetAggregateNameValidator(ITranslator translator)
    {
        //RuleFor(p => p.FirstName).MinimumLength(2).WithMessage(translator[ResourceKeys.InValidMinLengthError]);
        //RuleFor(p => p.LastName).MinimumLength(2).WithMessage(translator[ResourceKeys.InValidMinLengthError]);
    }
}
";
}