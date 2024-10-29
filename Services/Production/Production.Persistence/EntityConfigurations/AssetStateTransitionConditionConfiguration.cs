using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;
using Articles.EntityFrameworkCore;
using Articles.EntityFrameworkCore.EntityConfigurations;

namespace Production.Persistence.EntityConfigurations;

internal class AssetStateTransitionConditionConfiguration : EntityConfiguration<AssetStateTransitionCondition>
{
		public override void Configure(EntityTypeBuilder<AssetStateTransitionCondition> builder)
		{
				base.Configure(builder);

				builder.Property(e => e.ArticleStage).HasEnumConversion().IsRequired();
				builder.Property(e => e.ActionTypes).HasJsonReadOnlyListConversion().IsRequired(); //todo provide a version for enums
				builder.Property(e => e.AssetTypes).HasJsonReadOnlyListConversion().IsRequired();
		}
}
