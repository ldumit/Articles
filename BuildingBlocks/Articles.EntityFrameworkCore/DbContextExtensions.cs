using Articles.Entitities;
using Articles.System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Articles.EntityFrameworkCore;

public static class DbContextExtensions
{
		public static void Seed<TEntity>(this DbContext context, string folderPath = "TestData")
				//where TEntity : Entity<int>
				where TEntity : class
		{
				if (context.Set<TEntity>().Any())
						return;

				var filePath = $"{AppContext.BaseDirectory}{folderPath}/{typeof(TEntity).Name}.json";
				if (File.Exists(filePath))
				{
						var collection = JsonExtensions.DeserializeCaseInsensitive<TEntity[]>(File.ReadAllText(filePath));
						if (collection != null)
								context.Set<TEntity>().AddRange(collection);
				}
				try
				{
						context.SaveChanges();
				}
				catch (Exception ex)
				{
						context.Database.ExecuteSql($"DBCC CHECKIDENT({typeof(TEntity).Name}, RESEED, 0)");
						Console.WriteLine(ex.Message);
						throw;
				}
		}

		public static void Migrate<TDbContext>(this WebApplication app)
				where TDbContext : DbContext
		{
				using var scope = app.Services.CreateScope();

				var context = scope.ServiceProvider.GetRequiredService<TDbContext>();

				context.Database.Migrate();
		}
}
