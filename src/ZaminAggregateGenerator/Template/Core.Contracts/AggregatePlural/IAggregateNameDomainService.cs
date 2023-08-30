using ZaminAggregateGenerator;

internal class IAggregateNameDomainService : ISourceCode
{
    public string GetClassPath() => @"Core.Contracts\AggregatePlural";
    public string GetSourceCode() => @"using Zamin.Extensions.DependencyInjection.Abstractions;

namespace ProjectName.Core.Contracts.AggregatePlural;

public interface IAggregateNameDomainService : IScopeLifetime
{
}
";
}