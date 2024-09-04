using Articles.System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Articles.EntityFrameworkCore;
using Production.Domain.Entities;
using Production.Persistence.EntityConfigurations;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace Production.Persistence;

public partial class ProductionDbContext(DbContextOptions<ProductionDbContext> _options, IMemoryCache _cache)
    : DbContext(_options)
{

    #region Entities
    public virtual DbSet<Article> Articles { get; set; }

		public virtual DbSet<ArticleActor> ArticleActors { get; set; }

		public virtual DbSet<Asset> Assets { get; set; }

    //public virtual DbSet<AssetCategory> AssetCategories { get; set; }

    //public virtual DbSet<AssetCategoryCode> AssetCategoryCodes { get; set; }


    //public virtual DbSet<AssetStatus> AssetStatuses { get; set; }


    //public virtual DbSet<AssetStatusCode> AssetStatusCodes { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    //public virtual DbSet<AssetTypeCode> AssetTypeCodes { get; set; }


    public virtual DbSet<Comment> Comments { get; set; }

    //public virtual DbSet<CommentType> CommentTypes { get; set; }

    //public virtual DbSet<CommentTypeCode> CommentTypeCodes { get; set; }

    public virtual DbSet<FileAction> FileActions { get; set; }

    //public virtual DbSet<FileActionType> FileActionTypes { get; set; }

    //public virtual DbSet<FileActionTypeCode> FileActionTypeCodes { get; set; }

    //public virtual DbSet<FileStatus> FileStatuses { get; set; }

    //public virtual DbSet<FileStatusCode> FileStatusCodes { get; set; }


    public virtual DbSet<Journal> Journals { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    //public virtual DbSet<StageCode> StageCodes { get; set; }

    public virtual DbSet<StageHistory> StageHistories { get; set; }


		public virtual DbSet<Person> Persons { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Typesetter> Typesetters { get; set; }
		//public virtual IQueryable<Typesetter> Typesetters => Persons.OfType<Typesetter>();

		///public virtual DbSet<User> Users { get; set; }
		#endregion

		public virtual IEnumerable<TEntity> GetCached<TEntity>()
        where TEntity : class
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
        //modelBuilder.HasDefaultSchema("public");

        //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        //{
        //    if (typeof(IMultitenancy).IsAssignableFrom(entityType.ClrType))
        //    {
        //        var entityBuilder = modelBuilder.Entity(entityType.ClrType);
        //        entityBuilder.AddQueryFilter<IMultitenancy>(e => e.TenantId.Equals(TenantId));
        //    }
        //}

        modelBuilder.ApplyConfiguration(new ArticleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleCurrentStageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AssetEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new AssetLatestFileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AssetTypeEntityConnfiguration());
				modelBuilder.ApplyConfiguration(new ArticleActorEntityConfiguration());
				modelBuilder.ApplyConfiguration(new AuthorEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new CommentEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new FileActionEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new FileEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new FileLatestActionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new JournalEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new StageEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new StageHistoryEntityConnfiguration());
        modelBuilder.ApplyConfiguration(new TypesetterEntityConnfiguration());
				modelBuilder.ApplyConfiguration(new PersonEntityConfiguration());
				//modelBuilder.ApplyConfiguration(new UserEntityConfiguration());


				foreach (var entity in modelBuilder.Model.GetEntityTypes())
				{
						var baseType = entity.BaseType;
						if (baseType == null) // check if we have inheritance, in that case we need to use the base class name.
								modelBuilder.Entity(entity.ClrType).ToTable(entity.ClrType.Name);
						else
								modelBuilder.Entity(entity.ClrType).ToTable(baseType.ClrType.Name);
				}

				//foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
				//{
				//    foreach (IMutableProperty property in entity.GetProperties()
				//        .Where(p => p.PropertyInfo != null && p.PropertyInfo.DeclaringType != null))
				//    {
				//        property.SetColumnName(property.PropertyInfo.Name.ToCamelCase());
				//    }
				//}
				base.OnModelCreating(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}