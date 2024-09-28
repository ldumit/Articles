using Articles.System;
using Microsoft.EntityFrameworkCore;
using Production.Domain.Entities;
using Production.Persistence.EntityConfigurations;
using Microsoft.Extensions.Caching.Memory;
using Articles.System.Cache;
using Articles.EntityFrameworkCore;

namespace Production.Persistence;

public partial class ProductionDbContext(DbContextOptions<ProductionDbContext> _options, IMemoryCache _cache)
    : DbContext(_options)
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

		public virtual IEnumerable<TEntity> GetCached<TEntity>()
        where TEntity : class, ICacheable
				=> _cache.GetOrCreate(entry => this.Set<TEntity>().ToList());

    public async Task<int> TrySaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await SaveChangesImpl(cancellationToken);
        }
        catch (Exception ex) 
        {
            //Log.Error(ex, ex.Message);
            return -1;
        }
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (base.Database.CurrentTransaction == null)
        {
            using var transaction = base.Database.BeginTransaction();
            var counter = await SaveChangesAndDispatchEventsAsync(cancellationToken);
            transaction.Commit();

            return counter;
        }
        else
        {
            return await SaveChangesAndDispatchEventsAsync(cancellationToken);
        }
        
    }

    private async Task<int> SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default)
    {
        // save first the main changes
        int counter = await SaveChangesImpl(cancellationToken);

        //todo implement events dispatching with fastendpoints
        int dispatchedEventsCounter = 0;
				//int dispatchedEventsCounter = (await _mediator.DispatchDomainEventsAsync(this));
        if (dispatchedEventsCounter > 0)
            // save changes from event handlers
            // todo domain events handlers should save their own changes
            await TrySaveChangesAsync(cancellationToken);

        return counter;
    }

    private async Task<int> SaveChangesImpl(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
				//modelBuilder.HasDefaultSchema("production");


				//todo use the following line:
				//modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

				modelBuilder.ApplyConfiguration(new ArticleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleCurrentStageEntityConfiguration());
				modelBuilder.ApplyConfiguration(new ArticleStageTransitionConfiguration());
				modelBuilder.ApplyConfiguration(new AssetEntityConfiguration());
				modelBuilder.ApplyConfiguration(new AssetStateTransitionConfiguration());
				modelBuilder.ApplyConfiguration(new AssetStateTransitionConditionConfiguration());
				modelBuilder.ApplyConfiguration(new AssetLatestFileEntityConfiguration());
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
}