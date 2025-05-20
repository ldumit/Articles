using Blocks.Core;
using Blocks.Core.Cache;
using Blocks.Core.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Blocks.EntityFrameworkCore;

public static class DbContextExtensions
{
		public static readonly JsonSerializerSettings DefaultSettings;

		static DbContextExtensions()
		{
				DefaultSettings = new JsonSerializerSettings
				{
						ContractResolver = new PrivateContractResolver()
				};
		}

		public static TEntity[] LoadFromJson<TEntity>(this DbContext context, string folderPath = "Data/Test")
				where TEntity : class
		{
				if (context.Set<TEntity>().Any())
						return [];

				var filePath = $"{AppContext.BaseDirectory}{folderPath}/{typeof(TEntity).Name}.json";
				if (!File.Exists(filePath))
						return [];

				return JsonConvert.DeserializeObject<TEntity[]>(File.ReadAllText(filePath), DefaultSettings) ?? [];
		}

		public static void SeedFromJson<TEntity>(this DbContext context, string folderPath = "Data/Test")
				where TEntity : class
		{
				if (context.Set<TEntity>().Any())
						return;

				var filePath = $"{AppContext.BaseDirectory}{folderPath}/{typeof(TEntity).Name}.json";
				if (!File.Exists(filePath))
						return;

				var rawJson = File.ReadAllText(filePath);
				var collection = JsonConvert.DeserializeObject<TEntity[]>(rawJson, DefaultSettings);
				if (!collection.IsNullOrEmpty())
						context.Set<TEntity>().AddRange(collection!);
				
				try { context.SaveChanges(); }
				catch (Exception ex)
				{
						var tableName = context.Model.FindEntityType(typeof(TEntity))?.GetTableName();
						if (tableName is not null)
								try { context.Database.ExecuteSql($"DBCC CHECKIDENT({tableName}, RESEED, 0)"); } catch { };
						Console.WriteLine(ex.Message);
						throw;
				}
		}

		public static void UnTrackCacheableEntities(this DbContext context)
		{
				foreach (var entry in context.ChangeTracker.Entries())
				{
						if (entry.Entity is ICacheable)
								entry.State = EntityState.Unchanged; // Mark as Unchanged to prevent modifications
				}
		}

		public static WebApplication Migrate<TDbContext>(this WebApplication app)
				where TDbContext : DbContext
		{
				using var scope = app.Services.CreateScope();

				var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

				context.Database.Migrate();
				return app;
		}

		//todo - use this generic method in all microservices
		public static void SeedTestData<TContext>(this IServiceProvider services, Action<TContext> seeder)
				where TContext : DbContext
		{
				using var scope = services.CreateScope();
				var context = scope.ServiceProvider.GetRequiredService<TContext>();
				seeder(context);
		}
}
