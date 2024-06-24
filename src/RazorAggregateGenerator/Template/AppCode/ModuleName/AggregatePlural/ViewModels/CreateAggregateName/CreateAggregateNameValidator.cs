using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameValidator : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\ViewModels\CreateAggregateName";
    public string GetSourceCode() => @"using FluentValidation;

namespace ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.CreateAggregateName;

public class CreateAggregateNameValidator : AbstractValidator<CreateAggregateNameViewModel>
{
    public CreateAggregateNameValidator()
    {

    }
}
";
}