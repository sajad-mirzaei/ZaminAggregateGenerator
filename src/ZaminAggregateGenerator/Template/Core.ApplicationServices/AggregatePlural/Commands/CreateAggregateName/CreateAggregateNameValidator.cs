﻿using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameValidator : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\CreateAggregateName";
    public string GetSourceCode() => @"using FluentValidation;
using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Contracts.AggregatePlural.Commands.CreateAggregateName;
using ProjectName.Core.Domain.Common;
using Zamin.Extensions.Translations.Abstractions;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Commands.CreateAggregateName;

public class CreateAggregateNameValidator : AbstractValidator<CreateAggregateNameCommand>
{
    public CreateAggregateNameValidator(ITranslator translator, IAggregateNameCommandRepository aggregateNameCommandRepository)
    {
        /*RuleFor(p => p.FirstName).NotNull().WithMessage(translator[ResourceKeys.MustNotNullError]);
        RuleFor(p => p.LastName).NotNull().WithMessage(translator[ResourceKeys.MustNotNullError]);*/
    }
}
";
}