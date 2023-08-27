using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.TemplateReplacement;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    private readonly string aggregateGeneratorPath;
    private readonly TemplateCopy templateCopy;

    public AggregateGenerator(AggregateGeneratorModel aggregateGeneratorModel)
    {
        aggregateGeneratorPath = ClassFilePath;
        templateCopy = new TemplateCopy(aggregateGeneratorModel);
    }

    public void Generate()
    {
        Configs.AggregateGeneratorPath = aggregateGeneratorPath;
        templateCopy.PerformCopy();
    }

    //در صورتی این فیلد مسیر درست را ارسال میکند که مورد زیر برای فایل های سمپل انجام شود
    //.csharp file -> click right -> Properties -> Copy to Output Directory -> Copy if newer
    private static string ClassFilePath => Path.GetDirectoryName(typeof(AggregateGenerator).Assembly.Location);
}