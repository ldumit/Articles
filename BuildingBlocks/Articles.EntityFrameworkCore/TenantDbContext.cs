using Articles.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Articles.EntityFrameworkCore;

internal class TenantDbContext : DbContext, IMultitenancy
{
		public int TenantId { get; set; }

		public TenantDbContext(DbContextOptions<TenantDbContext> options, IOptions<TenantOptions> tenantOptions)
				: base(options)
		{
				TenantId = tenantOptions.Value.TenantId;
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
				SetTenantId();
				return base.SaveChangesAsync(cancellationToken);
		}

		private void SetTenantId()
		{
				if (!TenantId.Equals(0))
				{
						//this.ChangeTracker.DetectChanges();
						var entries = this.ChangeTracker.Entries()
								.Where(t => t.State == EntityState.Added);

						foreach (var entry in entries)
						{
								if (entry.Entity is IMultitenancy)
								{
										((IMultitenancy)entry.Entity).TenantId = TenantId;
								}
						}
				}
		}
}
