using ZaminAggregateGenerator.Services;

internal class CreateAggregateNameViewModel : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\ViewModels\CreateAggregateName";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.CreateAggregateName;

public class CreateAggregateNameViewModel : BaseViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? DetailId { get; set; }
    public byte StatusId { get; set; }
    public int? PictureId { get; set; }
}
";
}