using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blocks.Core;

public static class ServiceCollectionExtensions
{
		public static IServiceCollection AddImplementationsOf<T>(
				this IServiceCollection services,
				Assembly[]? assemblies = null,
				ServiceLifetime lifetime = ServiceLifetime.Scoped)
		{
				assemblies ??= new[] { Assembly.GetCallingAssembly() };

				var type = typeof(T);

				var implementations = assemblies
						.SelectMany(a => a.GetTypes())
						.Where(t => type.IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });

				foreach (var impl in implementations)
				{
						services.Add(new ServiceDescriptor(type, impl, lifetime));
				}

				return services;
		}

		public static IServiceCollection AddImplementationsOfGeneric(
				this IServiceCollection services,
				Type genericBaseType,
				Assembly[]? assemblies = null,
				ServiceLifetime lifetime = ServiceLifetime.Scoped)
		{
				assemblies ??= new[] { Assembly.GetCallingAssembly() };

				var types = assemblies
						.SelectMany(a => a.GetTypes())
						.Where(t =>
								t.IsClass &&
								!t.IsAbstract &&
								t.BaseType != null &&
								IsSubclassOfRawGeneric(t, genericBaseType));

				foreach (var impl in types)
				{
						var baseType = impl.BaseType!;
						services.Add(new ServiceDescriptor(baseType, impl, lifetime));
				}

				return services;
		}

		private static bool IsSubclassOfRawGeneric(Type type, Type genericBase)
		{
				while (type != null && type != typeof(object))
				{
						var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
						if (genericBase == cur)
								return true;
						type = type.BaseType!;
				}
				return false;
		}

		public static IServiceCollection AddConcreteImplementationsOfGeneric(
				this IServiceCollection services,
				Type genericBaseType,
				Assembly[]? assemblies = null,
				ServiceLifetime lifetime = ServiceLifetime.Scoped)
		{
				assemblies ??= new[] { Assembly.GetCallingAssembly() };

				var implementations = assemblies
						.SelectMany(a => a.GetTypes())
						.Where(t =>
								t.IsClass &&
								!t.IsAbstract &&
								InheritsFromGeneric(t, genericBaseType));

				foreach (var impl in implementations)
				{
						services.Add(new ServiceDescriptor(impl, impl, lifetime));
				}

				return services;
		}

		private static bool InheritsFromGeneric(Type type, Type genericBase)
		{
				while (type != null && type != typeof(object))
				{
						var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
						if (cur == genericBase)
								return true;
						type = type.BaseType;
				}
				return false;
		}

}

