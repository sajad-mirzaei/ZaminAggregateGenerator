using ZaminAggregateGenerator.Services;

internal class AddEntityNameValidator : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Commands\AddEntityName";
    public string GetSourceCode() => @"using ProjectName.Core.Contracts.AggregatePlural.Commands;
using ProjectName.Core.Contracts.AggregatePlural.Commands.AddEntityName;
using FluentValidation;

namespace ProjectName.Core.ApplicationServices.AggregatePlural.Commands.AddEntityName;

public class AddEntityNameValidator : AbstractValidator<AddEntityNameCommand>
{
    public AddEntityNameValidator(ITranslator translator, IAggregateNameCommandRepository aggregateNameCommandRepository)
    {
        /*RuleFor(p => p.FirstName).NotNull().WithMessage(translator[ResourceKeys.MustNotNullError]);
        RuleFor(p => p.LastName).NotNull().WithMessage(translator[ResourceKeys.MustNotNullError]);*/
    }
}
";
}