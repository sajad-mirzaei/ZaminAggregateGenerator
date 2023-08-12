﻿using ZaminAggregateGenerator.Models;
using ZaminAggregateGenerator.TemplateContentChange;
using ZaminAggregateGenerator.Tools;

namespace ZaminAggregateGenerator;

public class AggregateGenerator
{
    private readonly string aggregateGeneratorPath;
    private readonly TemplateCopy templateCopy;

    public AggregateGenerator(AggregateGeneratorModel aggregateGeneratorModel)
    {
        aggregateGeneratorPath = "D:\\.NET\\Github\\MyGithub\\voc\\ZaminAggregateGenerator";
        templateCopy = new TemplateCopy(aggregateGeneratorModel);
    }

    public void Generate()
    {
        Configs.AggregateGeneratorPath = aggregateGeneratorPath;
        templateCopy.PerformCopy();
    }
}