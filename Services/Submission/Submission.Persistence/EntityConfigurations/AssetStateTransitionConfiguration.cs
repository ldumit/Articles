using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;
using Articles.EntityFrameworkCore;
using Articles.EntityFrameworkCore.EntityConfigurations;

namespace Submission.Persistence.EntityConfigurations;

internal class AssetStateTransitionConfiguration : MetadataConfiguration<AssetStateTransition>
{
		public override void Configure(EntityTypeBuilder<AssetStateTransition> builder)
		{
				base.Configure(builder);

				builder.HasKey(e => new {e.CurrentState, e.ActionType, e.DestinationState});

				builder.Property(e => e.CurrentState).IsRequired().HasEnumConversion();
				builder.Property(e => e.DestinationState).IsRequired().HasEnumConversion();
				builder.Property(e => e.ActionType).IsRequired().HasEnumConversion();
		}
}
