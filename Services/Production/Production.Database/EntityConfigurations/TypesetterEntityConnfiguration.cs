using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Database.EntityConfigurations;

public class TypesetterEntityConnfiguration : IEntityTypeConfiguration<Typesetter>
{
    public  void Configure(EntityTypeBuilder<Typesetter> entity)
    {
        entity.HasKey(e => new { e.TenantId, e.UserId }).HasName("typesetter_pkey");

        entity.ToTable("typesetter");

        entity.Property(e => e.TenantId);
        entity.Property(e => e.UserId);
        entity.Property(e => e.CreationDate)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            ;
        entity.Property(e => e.IsDefault)
            .HasDefaultValueSql("false")
            ;
        entity.Property(e => e.ModificationDate)
            .HasColumnType("timestamp without time zone")
            ;

        //entity.HasOne(d => d.User).WithMany(p => p.Typesetters)
        //    .HasForeignKey(d => d.UserId)
        //    .OnDelete(DeleteBehavior.Restrict)
        //    .HasConstraintName("typesetter_id_fkey");
        entity.Ignore(t => t.Id);
    }
}
