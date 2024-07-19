using Articles.Entitities;
using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class CommentEntityConnfiguration : EntityConfigurationBase<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> entity)
    {
        base.Configure(entity);

        entity.HasIndex(e => new { e.ArticleId, e.TypeId }).IsUnique();

        entity.Property(e => e.Text).HasMaxLength(Constraints.TwoTousands);
        entity.Property(e => e.TypeId).HasConversion<int>().IsRequired();
    }
}
