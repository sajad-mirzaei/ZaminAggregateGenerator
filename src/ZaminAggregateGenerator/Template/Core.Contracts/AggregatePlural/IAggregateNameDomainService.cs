public class IAggregateNameDomainService : ISourceCode
{
    public string GetSourceCode() => @"using Zamin.Extensions.DependencyInjection.Abstractions;

namespace ProjectName.Core.Contracts.AggregatePlural;

public interface IAggregateNameDomainService : IScopeLifetime
{
}
";
}