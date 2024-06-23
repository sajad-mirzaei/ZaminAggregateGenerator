using ZaminAggregateGenerator.Services;

namespace RazorAggregateGenerator.Services;

internal static class Configs
{
    internal static Dictionary<string, List<ISourceCode>> LayerMappings { get; set; } = new()
    {
        {
            "AppCode",
            new List<ISourceCode>
            {
                new CreateAggregateNameCommand(),
                new IAggregateNameService(),
                new AggregateNameByIdDto(),
                new GetAggregateNameByIdQuery(),
                new CreateAggregateNameValidator(),
                new CreateAggregateNameViewModel(),
                new GetAggregateNameViewModel(),
                new AggregateNameConfig(),
                new AggregateNameService()
            }
        },
        {
            "Pages",
            new List<ISourceCode>
            {
                new AggregateName__cshtml__cs(),
                new AggregateName__cshtml(),
                new AggregateName__cshtml__js()
            }
        }
    };
}
