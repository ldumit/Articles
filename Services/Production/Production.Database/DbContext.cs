using Articles.Entitities;
using Articles.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Articles.EntityFrameworkCore;
using Production.Domain.Entities;
using Production.Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Production.Database;

public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext, IMultitenancy
{
    private readonly IMediator _mediator;

    public int TenantId { get; set; }
    public DbContext(DbContextOptions<DbContext> options):base(options)
    {
            
    }
    public DbContext(DbContextOptions<DbContext> options, IOptions<TenantConfig> tenantConfig, IMediator mediator)
        : base(options)
    {
        _mediator = mediator;

        TenantId = tenantConfig.Value.TenantId;
    }

    #region Entities
    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Asset> Assets { get; set; }

    //public virtual DbSet<AssetCategory> AssetCategories { get; set; }

    //public virtual DbSet<AssetCategoryCode> AssetCategoryCodes { get; set; }


    public virtual DbSet<AssetStatus> AssetStatuses { get; set; }


    public virtual DbSet<AssetStatusCode> AssetStatusCodes { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    //public virtual DbSet<AssetTypeCode> AssetTypeCodes { get; set; }


    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentType> CommentTypes { get; set; }

    public virtual DbSet<CommentTypeCode> CommentTypeCodes { get; set; }

    public virtual DbSet<FileAction> FileActions { get; set; }

    public virtual DbSet<FileActionType> FileActionTypes { get; set; }

    public virtual DbSet<FileActionTypeCode> FileActionTypeCodes { get; set; }

    public virtual DbSet<FileStatus> FileStatuses { get; set; }

    public virtual DbSet<FileStatusCode> FileStatusCodes { get; set; }


    public virtual DbSet<Journal> Journals { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    public virtual DbSet<StageCode> StageCodes { get; set; }

    public virtual DbSet<StageHistory> StageHistories { get; set; }


    public virtual DbSet<Typesetter> Typesetters { get; set; }


    public virtual DbSet<User> Users { get; set; }


    #endregion


    //todo
    [Obsolete("Use the async method because it dispatches the domain events also")]
    public override int SaveChanges()
    {
        SetSpaceId();

        return base.SaveChanges();
    }

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

        int dispatchedEventsCounter = (await _mediator.DispatchDomainEventsAsync(this));
        if (dispatchedEventsCounter > 0)
            // save changes from event handlers
            // todo domain events handlers should save their own changes
            await TrySaveChangesAsync(cancellationToken);

        return counter;
    }

    private async Task<int> SaveChangesImpl(CancellationToken cancellationToken = default)
    {
        SetSpaceId();

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetSpaceId()
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("public");

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IMultitenancy).IsAssignableFrom(entityType.ClrType))
            {
                var entityBuilder = modelBuilder.Entity(entityType.ClrType);
                entityBuilder.AddQueryFilter<IMultitenancy>(e => e.TenantId.Equals(TenantId));
            }
        }

        modelBuilder.ApplyConfiguration(new ArticleEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new AssetEntityConnfiguration()); 
        //modelBuilder.ApplyConfiguration(new ArticleAssetTypeFileExtensionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleFigshareDepositionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleJobEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleReDepositionConfigurationEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleRepositoryEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleRepositoryFtpconfigurationEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleRepositoryFtppathEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleRepositoryJournalMappingEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleRepositorySftpconfigurationEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleRepositoryTypeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleTypesetterEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetCategoryEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetCategoryCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetRoleActionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetStageActionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetStatusEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetStatusActionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetStatusCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetTypeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetTypeCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetTypeSourceEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetTypeSourceCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AssetTypeVisibilityEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new AuthorEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new CommentEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new CommentTypeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new CommentTypeCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new DepositedFileEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new DepositionJobStatusEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new DepositionJobStatusCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FigshareDepositionStatusEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FigshareDepositionStatusCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FileEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FileActionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FileActionTypeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FileActionTypeCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FileExtensionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FileStatusEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new FileStatusCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ImpersonationActionHistoryEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new JournalEntityConnfiguration(TenantId));
        //modelBuilder.ApplyConfiguration(new JournalFigshareMappingEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new JournalTypesetterEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedArticleEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedArticleContentEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedAuthorAffiliationEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedAuthorDetailEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedFileEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedStatusEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedStatusesCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new RoleCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new StageEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new StageCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new StageHistoryEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new SubmissionEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new TypesetterEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new UploadRequestEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new UploadRequestFileEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new UserEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ZipContentFileEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ZipContentFileTypeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new ZipContentFileTypeCodeEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new WorkflowEventEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new WorkflowEventParameterEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedKeywordsConfiguration());
        //modelBuilder.ApplyConfiguration(new PublishedArticleKeywordsConfiguration());
        //modelBuilder.ApplyConfiguration(new GroupTypeEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new GroupUserEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new MessageEntityConfiguraion());
        //modelBuilder.ApplyConfiguration(new PublishedSupplementaryFilesEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleAssociateEditorEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleReviewEditorEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new EmailHistoryConfiguration());
        //modelBuilder.ApplyConfiguration(new ActionMessageTemplateEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleHistoryEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleHistorySourceConfigurationEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleHistoryVisibilityEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleHistorySourceTypeEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new SecurityRoleEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new DomainEmailEntityConnfiguration());
        //modelBuilder.ApplyConfiguration(new UploadBatchEntityConfiguration());
        //modelBuilder.ApplyConfiguration(new ArticleIntegrationEventConfiguration());
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (IMutableProperty property in entity.GetProperties()
                .Where(p => p.PropertyInfo != null && p.PropertyInfo.DeclaringType != null))
            {
                property.SetColumnName(property.PropertyInfo.Name.ToCamelCase());
            }
        }
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}