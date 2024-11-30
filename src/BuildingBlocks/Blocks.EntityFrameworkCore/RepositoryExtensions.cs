using Blocks.Entitities;
using Blocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFrameworkCore
{
		public static class RepositoryExtensions
		{
				// talk about why we is better to create an extension method in this case.
				public static async Task<TEntity> FindByIdOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id)
						where TContext : DbContext
						where TEntity : class, IEntity<int>
				{
						var entity = await repository.FindByIdAsync(id);
						if (entity is null)
								throw new NotFoundException($"{typeof(TEntity).Name} not found");
						return entity!;
				}

				public static async Task<TEntity> FindByIdOrThrowAsync<TEntity>(this DbSet<TEntity> dbSet, int id)
						where TEntity : class, IEntity<int>
				{
						var entity = await dbSet.FindAsync(id);
						if (entity is null)
								throw new NotFoundException($"{typeof(TEntity).Name} not found");
						return entity!;
				}

				//talk about the difference between FindByIdAsync and GetByIdAsync
				public static async Task<TEntity> GetByIdOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, int id)
						where TContext : DbContext
						where TEntity : class, IEntity<int>
				{
						var entity = await repository.GetByIdAsync(id, false);
						if (entity is null)
								throw new NotFoundException($"{typeof(TEntity).Name} not found");
						return entity!;
				}

				public static async Task<TEntity> GetByIdOrThrowAsync<TEntity, TContext>(this Repository<TContext, TEntity> repository, Func<int, Task<TEntity>> getByIdFunc, int id)
						where TContext : DbContext
						where TEntity : class, IEntity<int>
				{
						var entity = await getByIdFunc(id);
						if (entity is null)
								throw new NotFoundException($"{typeof(TEntity).Name} not found");
						return entity!;
				}
		}
}
