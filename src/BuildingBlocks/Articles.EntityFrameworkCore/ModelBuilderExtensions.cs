using Blocks.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFrameworkCore
{
		public static class ModelBuilderExtensions
		{
				public static void UseClrTypeNamesForTables(this ModelBuilder modelBuilder)
				{
						foreach (var entity in modelBuilder.Model.GetEntityTypes())
						{
								if (typeof(IValueObject).IsAssignableFrom(entity.ClrType)) // we don't create tables for value objects
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

				//todo the following methods don't work because the propertyBuilder is for property Value not for the complex type
				//public static ComplexTypePropertyBuilder<TProperty> HasColumnNameUsingPropertyName<TProperty>(this ComplexTypePropertyBuilder<TProperty> propertyBuilder)
				//{
				//		return propertyBuilder.HasColumnName(propertyBuilder.Metadata.PropertyInfo!.Name);
				//}

				//public static ComplexTypePropertyBuilder<TProperty> HasColumnNameUsingTypeAndPropertyName<TProperty>(this ComplexTypePropertyBuilder<TProperty> propertyBuilder)
				//		=> propertyBuilder.HasColumnName($"" +
				//				$"{propertyBuilder.Metadata.DeclaringType!.Name}_" +
				//				$"{propertyBuilder.Metadata.PropertyInfo!.Name}");
		}
}
