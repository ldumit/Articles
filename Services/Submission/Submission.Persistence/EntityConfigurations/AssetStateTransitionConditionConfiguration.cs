using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;
using Articles.EntityFrameworkCore;
using Articles.EntityFrameworkCore.EntityConfigurations;

namespace Submission.Persistence.EntityConfigurations;

internal class AssetStateTransitionConditionConfiguration : EntityConfiguration<AssetStateTransitionCondition>
{
		public override void Configure(EntityTypeBuilder<AssetStateTransitionCondition> builder)
		{
				base.Configure(builder);

				builder.Property(e => e.ArticleStage).HasEnumConversion().IsRequired();
				builder.Property(e => e.ActionTypes).HasJsonListConversion().IsRequired(); //todo provide a version for enums
				builder.Property(e => e.AssetTypes).HasJsonListConversion().IsRequired();
		}
}
