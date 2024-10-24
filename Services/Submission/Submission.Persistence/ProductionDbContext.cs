using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;
using Submission.Persistence.EntityConfigurations;
using Microsoft.Extensions.Caching.Memory;
using Articles.EntityFrameworkCore;

namespace Submission.Persistence;

public partial class ProductionDbContext(DbContextOptions<ProductionDbContext> options, IMemoryCache cache)
    : ApplicationDbContext<ProductionDbContext>(options, cache)
{

    #region Entities
    public virtual DbSet<Article> Articles { get; set; }

		public virtual DbSet<ArticleActor> ArticleActors { get; set; }
		public virtual DbSet<Asset> Assets { get; set; }
    public virtual DbSet<AssetType> AssetTypes { get; set; }

    //public virtual DbSet<FileAction> FileActions { get; set; }

    public virtual DbSet<Journal> Journals { get; set; }
    public virtual DbSet<Stage> Stages { get; set; }
    public virtual DbSet<StageHistory> StageHistories { get; set; }
		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Typesetter> Typesetters { get; set; }
		#endregion


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

				//todo use the following line:
				//modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

				modelBuilder.ApplyConfiguration(new ArticleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleCurrentStageEntityConfiguration());
				modelBuilder.ApplyConfiguration(new ArticleStageTransitionConfiguration());
				modelBuilder.ApplyConfiguration(new AssetEntityConfiguration());
				modelBuilder.ApplyConfiguration(new AssetStateTransitionConfiguration());
				modelBuilder.ApplyConfiguration(new AssetStateTransitionConditionConfiguration());
				modelBuilder.ApplyConfiguration(new AssetCurrentFileLinkEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AssetActionEntityConfiguration());
				modelBuilder.ApplyConfiguration(new AssetTypeEntityConfiguration());
				modelBuilder.ApplyConfiguration(new ArticleActorEntityConfiguration());
				modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FileActionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new FileLatestActionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new JournalEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StageHistoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TypesetterEntityConfiguration());
				modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());

        //modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());


        modelBuilder.UseClrTypeNamesForTables();


				base.OnModelCreating(modelBuilder);
    }

		public async override Task<int> SaveChangesAsync(CancellationToken ct = default)
		{
				this.UnTrackCacheableEntities();

				return await base.SaveChangesAsync(ct);
		}
}