﻿using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class AssetActionEntityConfiguration : EntityConfiguration<AssetAction>
{
    public override void Configure(EntityTypeBuilder<AssetAction> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.AssetId);

        builder.Property(e => e.TypeId).HasEnumConversion();

        builder.HasOne(d => d.Asset).WithMany(p => p.Actions)
            .HasForeignKey(d => d.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
