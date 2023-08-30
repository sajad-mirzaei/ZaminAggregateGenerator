using ZaminAggregateGenerator;

internal class AggregateNameConfig : ISourceCode
{
    public string GetClassPath() => @"Infra.Data.Sql.Commands\AggregatePlural\Configs";
    public string GetSourceCode() => @"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectName.Core.Domain.AggregatePlural.Entities;
using ProjectName.Infra.Data.Sql.Commands.Common.AuditableShadowProperty.Extensions;

namespace ProjectName.Infra.Data.Sql.Commands.AggregatePlural.Configs;

public class AggregateNameConfig : IEntityTypeConfiguration<AggregateName>
{
	public void Configure(EntityTypeBuilder<AggregateName> builder)
	{
		builder.ToTable(""AggregatePlural"");
		//builder.Property(c => c.Id).HasColumnName(""ccAggregateName"");

		//builder.DisableShadowProperty();
	}
}
";
}