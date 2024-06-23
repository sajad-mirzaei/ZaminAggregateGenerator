using ZaminAggregateGenerator.Services;

internal class GetAggregateNameViewModel : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural\ViewModels\GetAggregatePlural";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.Compacts.AggregatePlural.ViewModels.GetAggregatePlural;

public class GetAggregateNameViewModel : BaseViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? DetailId { get; set; }
    public byte StatusId { get; set; }
    public int? PictureId { get; set; }
}
";
}