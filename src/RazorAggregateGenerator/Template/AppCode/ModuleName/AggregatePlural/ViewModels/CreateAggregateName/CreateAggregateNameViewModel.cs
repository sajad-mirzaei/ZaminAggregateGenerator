using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameViewModel : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\ViewModels\CreateAggregateName";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.ModuleName.AggregatePlural.ViewModels.CreateAggregateName;

public class CreateAggregateNameViewModel : BaseViewModel
{
AppCodeReplacementText1
}
";
}