﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

internal class AssetLatestFileEntityConfiguration : IEntityTypeConfiguration<AssetCurrentFileLink>
{
    public void Configure(EntityTypeBuilder<AssetCurrentFileLink> entity)
    {
        entity.HasKey(e => e.AssetId);
        entity.HasIndex(e => e.FileId).IsUnique();


        entity.HasOne(e => e.File).WithOne()
            .HasForeignKey<AssetCurrentFileLink>(e => e.FileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
