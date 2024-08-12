using Articles.Entitities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Articles.EntityFrameworkCore;

public static class DbContextExtensions
{
		//The files needs to be located in a TestData folder
		public static void Seed<TEntity>(this DbContext context)
				where TEntity : Entity<int>
		{
				if (context.Set<TEntity>().Any())
						return;

				var filePath = $"{AppContext.BaseDirectory}TestData/{typeof(TEntity).Name}.json";
				if (File.Exists(filePath))
				{
						var collection = JsonConvert.DeserializeObject<TEntity[]>(File.ReadAllText(filePath));
						if (collection != null)
								context.Set<TEntity>().AddRange(collection);
				}
				context.SaveChanges();
		}

		public static void Migrate<TDbContext>(this WebApplication app)
				where TDbContext : DbContext
		{
				using var scope = app.Services.CreateScope();

				var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

				context.Database.Migrate();
		}
}
