using Newtonsoft.Json;
using Redis.OM;

namespace Articles.Redis;

public static class Extensions
{
		public static void Seed<TEntity>(this RedisConnectionProvider provider, string folderPath = "TestData")
				//where TEntity : Entity<int>
				where TEntity : class
		{
				var collection = provider.RedisCollection<TEntity>();
				if (collection.Any())
						return;

				var filePath = $"{AppContext.BaseDirectory}{folderPath}/{typeof(TEntity).Name}.json";
				if (!File.Exists(filePath))
						return;

				var data = JsonConvert.DeserializeObject<TEntity[]>(File.ReadAllText(filePath));
				//var collection = JsonExtensions.DeserializeCaseInsensitive<TEntity[]>(File.ReadAllText(filePath));
				if (data != null)
						collection.InsertAsync(data);

				try { collection.SaveAsync(); }
				catch (Exception ex)
				{
						//collection.DeleteAsync(data);
						Console.WriteLine(ex.Message);
						throw;
				}
		}
}
