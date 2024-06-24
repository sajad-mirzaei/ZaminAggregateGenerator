using ZaminAggregateGenerator.Services;

internal class AggregateNameConfig : ISourceCode
{
    public string GetClassPath() => @"ModuleName\AggregatePlural";
    public string GetSourceCode() => @"namespace ProjectName.AppCode.ModuleName.AggregatePlural
{
    public class AggregateNameConfig
    {
        public const string GetAggregateNameByIdRoute = ""/api/AggregateName/getAggregateNameById"";
        public const string CreateAggregateNameRoute = ""/api/AggregateName/create"";
    }
}
";
}