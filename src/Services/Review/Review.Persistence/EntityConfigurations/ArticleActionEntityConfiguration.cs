using Review.Domain.Articles;

namespace Review.Persistence.EntityConfigurations;

public class ArticleActionEntityConfiguration : EntityConfiguration<ArticleAction>
{
    public override void Configure(EntityTypeBuilder<ArticleAction> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.EntityId);

        builder.Property(e => e.TypeId).HasEnumConversion();
    }
}
