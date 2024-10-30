using ZaminAggregateGenerator.Services;

internal class EntityNameConfig : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Configs";
    public string GetSourceCode() => @"using ProjectName.Core.Domain.AggregatePlural.Entities;
using Infra.Data.Sql.Commands.Common.Extensions;

namespace Infra.Data.Sql.Commands.AggregatePlural.Configs;

public class EntityNameConfig : IEntityTypeConfiguration<EntityName>
{
    public void Configure(EntityTypeBuilder<EntityName> builder)
    {
DisableShadowPropertyReplacementText6
        builder.ToTable(""EntityPlural"", ""dbo"");
        builder.HasKey(e => e.Id);
		//builder.Property(c => c.Id).HasColumnName(""""ccEntityName"""");
    }
}
";
}