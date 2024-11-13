using Blocks.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Blocks.Core.Cache;

namespace Service.Persistence;

public partial class ServiceDbContext(DbContextOptions<ServiceDbContext> _options, IMemoryCache _cache)
    : DbContext(_options)
{

    #region Entities

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