using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.TemplateManage;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    private readonly string aggregateGeneratorPath;
    private readonly TemplateCopy templateCopy;

    public AggregateGenerator(AggregateGeneratorModel aggregateGeneratorModel)
    {
        aggregateGeneratorPath = "D:\\.NET\\Github\\MyGithub\\ZaminAggregateGenerator\\src\\ZaminAggregateGenerator";
        templateCopy = new TemplateCopy(aggregateGeneratorModel);
    }

    public void Generate()
    {
        Configs.AggregateGeneratorPath = aggregateGeneratorPath;
        templateCopy.PerformCopy();
    }
}