using ZaminAggregateGenerator.Services;

internal class AggregateNameConfig : ISourceCode
{
    public string GetClassPath() => @"AggregatePlural\Configs";
    public string GetSourceCode() => @"using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectName.Core.Domain.AggregatePlural.Entities;
using ProjectName.Infra.Data.Sql.Commands.Common.Extensions;

namespace ProjectName.Infra.Data.Sql.Commands.AggregatePlural.Configs;

public class AggregateNameConfig : IEntityTypeConfiguration<AggregateName>
{
	public void Configure(EntityTypeBuilder<AggregateName> builder)
	{
DisableShadowPropertyReplacementText6
		builder.ToTable(""AggregatePlural"", ""dbo"");
		//builder.Property(c => c.Id).HasColumnName(""ccAggregateName"");
	}
}
";
}