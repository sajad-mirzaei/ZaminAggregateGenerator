using ZaminAggregateGenerator;

internal class CreateAggregateNameValidator : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\CreateAggregateName";
    public string GetSourceCode() => @"using FluentValidation;
using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Contracts.AggregatePlural.Commands.CreateAggregateName;
using ProjectName.Core.Domain.Common;
using Zamin.Extensions.Translations.Abstractions;

namespace ProjectName.Core.ApplicationService.AggregatePlural.Commands.CreateAggregateName;

public class CreateAggregateNameValidator : AbstractValidator<CreateAggregateNameCommand>
{
    [Obsolete]
    public CreateAggregateNameValidator(ITranslator translator, IAggregateNameCommandRepository aggregateNameCommandRepository)
    {
        this.CascadeMode = CascadeMode.Stop;
        /*RuleFor(p => p.FirstName).NotNull().WithMessage(translator[ResourceKeys.MustNotNullError]);
        RuleFor(p => p.LastName).NotNull().WithMessage(translator[ResourceKeys.MustNotNullError]);*/
    }
}
";
}