﻿using Blocks.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blocks.EntityFrameworkCore
{
		public static class ModelBuilderExtensions
		{
				public static void UseClrTypeNamesForTables(this ModelBuilder modelBuilder)
				{
						foreach (var entity in modelBuilder.Model.GetEntityTypes())
						{
								// we don't create tables for value objects
								if (typeof(IValueObject).IsAssignableFrom(entity.ClrType))
										continue;


								// Find the *root* base type (top of hierarchy) (for TPH strategy)
								var rootType = entity;
								while (rootType.BaseType != null)
								{
										rootType = rootType.BaseType;
								}

								modelBuilder.Entity(entity.ClrType).ToTable(rootType.ClrType.Name);
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

				public static void UseLowerCaseNamingConvention(this ModelBuilder modelBuilder)
				{
						foreach (var entity in modelBuilder.Model.GetEntityTypes())
						{
								entity.SetTableName(entity.GetTableName()?.ToLower());

								foreach (var property in entity.GetProperties())
								{
										property.SetColumnName(property.Name.ToLower());
								}

								foreach (var key in entity.GetKeys())
								{
										key.SetName(key.GetName()?.ToLower());
								}

								foreach (var foreignKey in entity.GetForeignKeys())
								{
										foreignKey.SetConstraintName(foreignKey.GetConstraintName()?.ToLower());
								}
						}
				}
		}
}
