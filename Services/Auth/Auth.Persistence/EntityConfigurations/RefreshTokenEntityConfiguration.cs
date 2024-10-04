using Articles.EntityFrameworkCore;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence;

internal class RefreshTokenEntityConfiguration : EntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.CreatedByIp).HasMaxLength(Constraints.C128);
        builder.Property(e => e.RevokedByIp).HasMaxLength(Constraints.C128);
		}
}
