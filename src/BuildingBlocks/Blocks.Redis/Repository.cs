using Redis.OM.Searching;
using Redis.OM;
using StackExchange.Redis;

namespace Blocks.Redis;

public class Repository<T>
		where T : Entity
{
		private readonly IRedisCollection<T> _collection;
		private readonly RedisConnectionProvider _provider;
		private readonly IDatabase _redisDb;

		public Repository(IConnectionMultiplexer redis, RedisConnectionProvider provider) =>
				(_redisDb, _provider, _collection) = (redis.GetDatabase(), provider, provider.RedisCollection<T>());

		public IRedisCollection<T> Collection => _collection;

		public async Task<T?> GetByIdAsync(int id) => await _collection.FindByIdAsync(id.ToString());
		public async Task<T> GetByIdOrThrowAsync(int id) => await _collection.GetByIdOrThrowAsync(id);

		public async Task<IEnumerable<T>> GetAllAsync() => await _collection.ToListAsync();

		public async Task AddAsync(T entity)
		{
				entity.Id = await GenerateNewId();
				await _collection.InsertAsync(entity);
		}

		public async Task UpdateAsync(T entity) => await _collection.UpdateAsync(entity);

		public async Task DeleteAsync(T entity) => await _collection.DeleteAsync(entity);
		public async Task SaveAllAsync() => await _collection.SaveAsync();

		public async Task<int> GenerateNewId() => (int) await _redisDb.StringIncrementAsync($"{typeof(T).Name}:Id:Sequence");
		public async Task<int> GenerateNewId<TOther>() => (int)await _redisDb.StringIncrementAsync($"{typeof(TOther).Name}:Id:Sequence");
		public async Task<int> SetNewId(Entity entity) => entity.Id = (int)await _redisDb.StringIncrementAsync($"{entity.GetType().Name}:Id:Sequence");

		//public async Task<IEnumerable<T>> SearchByNameAsync(string searchTerm)
		//{
		//		// Search using Redis.OM's built-in search capabilities
		//		var results = await _collection.Where(x => x.Name.Contains(searchTerm) || x.ShortName.Contains(searchTerm)).ToListAsync();
		//		return results;
		//}
}
