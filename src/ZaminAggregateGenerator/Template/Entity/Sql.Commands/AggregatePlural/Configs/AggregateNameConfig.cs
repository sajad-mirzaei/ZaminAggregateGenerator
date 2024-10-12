using ZaminAggregateGenerator.Services;

internal class AggregateNameConfig_Entity : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Configs";
    public string GetSourceCode() => @"using ProjectName.Core.Domain.AggregatePlural.Entities;
using Infra.Data.Sql.Commands.Common.Extensions;

namespace Infra.Data.Sql.Commands.AggregatePlural.Configs;

public class AggregateNameConfig : IEntityTypeConfiguration<AggregateName>
{
    public void Configure(EntityTypeBuilder<AggregateName> builder)
    {
        builder.DisableShadowProperty();
        builder.ToTable(""AggregatePlural"", ""dbo"");
        builder.HasMany<EntityName>()
            .WithOne()
            .HasPrincipalKey(c => c.Id)
            .HasForeignKey(""AggregateNameId"");
        //builder.Property(c => c.Id).HasColumnName(""ccAggregateName"");
    }
}
";
}
