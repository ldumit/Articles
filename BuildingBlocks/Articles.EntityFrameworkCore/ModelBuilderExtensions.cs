using Articles.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Articles.EntityFrameworkCore
{
		public static class ModelBuilderExtensions
		{
				public static void UseClrTypeNamesForTables(this ModelBuilder modelBuilder)
				{
						foreach (var entity in modelBuilder.Model.GetEntityTypes())
						{
								if (!typeof(IEntity).IsAssignableFrom(entity.ClrType)) // we don't create tables for value objects
										continue;

								var baseType = entity.BaseType;
								if (baseType == null) // check if we have inheritance, in that case we need to use the base class name.
										modelBuilder.Entity(entity.ClrType).ToTable(entity.ClrType.Name);
								else
										modelBuilder.Entity(entity.ClrType).ToTable(baseType.ClrType.Name);
						}
				}

				public static void UseCamelCaseForColumns(this ModelBuilder modelBuilder)
				{
						foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
						{
								foreach (IMutableProperty property in entity.GetProperties()
										.Where(p => p.PropertyInfo != null && p.PropertyInfo.DeclaringType != null))
								{
										property.SetColumnName(property.PropertyInfo.Name.ToCamelCase());
								}
						}
				}
		}
}
