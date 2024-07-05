using Common.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class StageHistoryEntityConnfiguration : TenantEntityConfigurationBase<StageHistory>
{
    protected override void ConfigureMore(EntityTypeBuilder<StageHistory> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.Id }).HasName("stageHistory_pkey");

        entity.ToTable("stageHistory");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.Id)
            .HasDefaultValueSql("nextval('\"ArticleStageHistory_ArticleStageHistoryId_seq\"'::regclass)")
            ;
        entity.Property(e => e.ArticleId);
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.StageId);
        entity.Property(e => e.StartDate);
        entity.HasOne(d => d.Article)
            .WithMany(p => p.StageHistories)
        .HasForeignKey(d => new { d.TenantId, d.ArticleId });
    }
}
