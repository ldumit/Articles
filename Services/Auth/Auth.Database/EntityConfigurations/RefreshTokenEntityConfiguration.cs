using Articles.EntityFrameworkCore;
using Auth.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence;

public class RefreshTokenEntityConfiguration : EntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.CreatedByIp).HasMaxLength(100);
        builder.Property(e => e.RevokedByIp).HasMaxLength(100);
    }
}
