using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class CommentEntityConfiguration : EntityConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => new { e.ArticleId, e.TypeId }).IsUnique();

        entity.Property(e => e.Text).HasMaxLength(Constraints.C2048);
        entity.Property(e => e.TypeId).HasConversion<int>().IsRequired();
    }
}
