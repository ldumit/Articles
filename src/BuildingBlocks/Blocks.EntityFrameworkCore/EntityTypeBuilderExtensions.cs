using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Blocks.Core;

namespace Blocks.EntityFrameworkCore;
public static class EntityTypeBuilderExtensions
{
		public static void Seed<T>(this EntityTypeBuilder<T> entity, string folder)
        where T : class
		{
				try
				{
						var stagesData = JsonExtensions.DeserializeCaseInsensitive<List<T>>(File.ReadAllText($@"../Production.Persistence/MasterData/{typeof(T).Name}.json"));
						if (stagesData != null)
								entity.HasData(stagesData);

				}
				catch (Exception ex)
				{
						Console.WriteLine("EX:---->" + ex.ToString());
				}
		}

		public static bool SeedFromFile<T>(this EntityTypeBuilder<T> builder, string folder = "Data/Master")
				where T : class
		{
				Console.WriteLine(AppContext.BaseDirectory);
				var filePath = $"{AppContext.BaseDirectory}{folder}/{typeof(T).Name}.json";
				if (!File.Exists(filePath))
						return false;
				var data = JsonExtensions.DeserializeCaseInsensitive<List<T>>(File.ReadAllText(filePath));				
				Console.WriteLine($"Seeding {data.Count} records for {typeof(T).Name}");
				//Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

				if (data != null)
						builder.HasData(data);

				return true;
		}

		public static void AddQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder, Expression<Func<T, bool>> expression)
    {
        var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
        var expressionFilter = ReplacingExpressionVisitor.Replace(
            expression.Parameters.Single(), parameterType, expression.Body);

        var currentQueryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
        if (currentQueryFilter != null)
        {
            var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
            expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
        }

        var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
        entityTypeBuilder.HasQueryFilter(lambdaExpression);
    }

    public static string ToCamelCase(this string propetyName)
    {
        if (string.IsNullOrEmpty(propetyName))
            return propetyName;

        return char.ToLowerInvariant(propetyName[0]) + propetyName.Substring(1);
		}
}
