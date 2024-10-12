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
        builder.DisableShadowProperty();
        builder.ToTable(""voc_goftegosatr"", ""dbo"");
        builder.HasKey(e => e.Id);
        builder.Property(c => c.Id).HasColumnName(""ccGoftegoSatr"");
        builder.Property(""AggregateNameId"").HasColumnName(""ccGoftego"");
        builder.Property(e => e.CreatedByUserId).HasColumnName(""ccUser"");
        builder.Property(e => e.Accessible)
            .HasDefaultValueSql(""('0')"")
            .HasColumnName(""dastresi"");
        builder.Property(e => e.Message)
            .IsRequired()
            .HasColumnType(""nvarchar(max)"")
            .HasColumnName(""matn"");
        builder.Property(e => e.CreatedDate)
            .HasDefaultValueSql(""(getdate())"")
            .HasColumnName(""tarikhEjad"");
    }
}
";
}