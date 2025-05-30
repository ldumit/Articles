﻿using Review.Persistence.EntityConfigurations;
using Microsoft.Extensions.Caching.Memory;

namespace Review.Persistence;

public partial class ReviewDbContext(DbContextOptions<ReviewDbContext> options, IMemoryCache cache)
    : ApplicationDbContext<ReviewDbContext>(options, cache)
{
    #region Entities
    public virtual DbSet<Article> Articles { get; set; }
		public virtual DbSet<ArticleContributor> ArticleContributors { get; set; }
		public virtual DbSet<Asset> Assets { get; set; }
    public virtual DbSet<AssetTypeDefinition> AssetTypes { get; set; }
    public virtual DbSet<Journal> Journals { get; set; }
    public virtual DbSet<Stage> Stages { get; set; }
    public virtual DbSet<StageHistory> StageHistories { get; set; }
		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Reviewer> Reviewers { get; set; }
		public virtual DbSet<ReviewInvitation> ReviewInvitations { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
				//todo use the following line:
				//modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

				modelBuilder.ApplyConfiguration(new ArticleEntityConfiguration());
				modelBuilder.ApplyConfiguration(new ArticleStageTransitionConfiguration());
				modelBuilder.ApplyConfiguration(new AssetEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleActionEntityConfiguration());
				modelBuilder.ApplyConfiguration(new AssetTypeDefinitionEntityConfiguration());
				modelBuilder.ApplyConfiguration(new ArticleContributorEntityConfiguration());
				modelBuilder.ApplyConfiguration(new ArticleAuthorEntityConfiguration());
				modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new JournalEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StageHistoryEntityConfiguration());
				modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());

        modelBuilder.UseClrTypeNamesForTables();

				base.OnModelCreating(modelBuilder);
    }

		public async override Task<int> SaveChangesAsync(CancellationToken ct = default)
		{
				this.UnTrackCacheableEntities();

				return await base.SaveChangesAsync(ct);
		}
}