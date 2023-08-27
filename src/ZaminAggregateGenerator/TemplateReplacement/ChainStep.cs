namespace ZaminAggregateGenerator.TemplateReplacement;

public abstract class ChainStep
{
    protected ChainStep _nextStep;

    public void SetNextStep(ChainStep nextStep)
    {
        _nextStep = nextStep;
    }

    public virtual string Process(string content)
    {
        if (_nextStep != null)
            content = _nextStep.Process(content);

        return ProcessStep(content);
    }

    protected abstract string ProcessStep(string content);
}
