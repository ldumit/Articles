using Articles.Entitities;
using Articles.Exceptions;
using Redis.OM.Searching;

namespace Journals.Persistence;

public static class RedisCollectionExtensions
{
		public static async Task<T?> GetByIdAsync<T>(this IRedisCollection<T> collection, int id)
				=> await collection.FindByIdAsync(id.ToString());

		public static async Task<T> GetByIdOrThrowAsync<T>(this IRedisCollection<T> collection, int id)
		{
				var entity = await collection.FindByIdAsync(id.ToString());
				if(entity is null) 
						throw new NotFoundException($"{typeof(T).Name} not found");
				return entity!;
		}

		//public static async Task AddAsync<T>(this IRedisCollection<T> collection, T entity)
		//		where T:Entity
		//{
		//		entity.Id = await GenerateNewId();
		//		await _collection.InsertAsync(entity);
		//}
}
