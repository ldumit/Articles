using Microsoft.Extensions.Caching.Memory;

namespace ArticleHub.Persistence;

public partial class ArticleHubDbContext(DbContextOptions<ArticleHubDbContext> options, IMemoryCache cache)
		: ApplicationDbContext<ArticleHubDbContext>(options, cache)
{
		#region Entities
		public virtual DbSet<Article> Articles { get; set; }
		public virtual DbSet<ArticleContributor> ArticleContributors { get; set; }
		public virtual DbSet<Journal> Journals { get; set; }
		public virtual DbSet<Person> Persons { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
				modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

				//modelBuilder.UseClrTypeNamesForTables();
				modelBuilder.UseLowerCaseNamingConvention(); //postgress standard

				base.OnModelCreating(modelBuilder);
		}

		public async override Task<int> SaveChangesAsync(CancellationToken ct = default)
		{
				this.UnTrackCacheableEntities();

				return await base.SaveChangesAsync(ct);
		}
}
