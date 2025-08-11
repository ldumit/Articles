using ArticleHub.Domain.Entities;
using ArticleHub.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
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
				//todo use the following line:
				//modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

				modelBuilder.ApplyConfiguration(new ArticleEntityConfiguration());
				modelBuilder.ApplyConfiguration(new ArticleContributorEntityConfiguration());
				modelBuilder.ApplyConfiguration(new JournalEntityConfiguration());
				modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());

				modelBuilder.UseClrTypeNamesForTables();
				modelBuilder.UseLowerCaseNamingConvention();

				base.OnModelCreating(modelBuilder);
		}

		public async override Task<int> SaveChangesAsync(CancellationToken ct = default)
		{
				this.UnTrackCacheableEntities();

				return await base.SaveChangesAsync(ct);
		}
}
