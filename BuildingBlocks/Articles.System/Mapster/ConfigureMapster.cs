using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Articles.Mapster;

public static class ConfigureMapster
{
		public static IServiceCollection AddMapster(this IServiceCollection services)
		{
				TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);

				//var typeAdapter = TypeAdapterConfig.GlobalSettings
				//	 .When((srcType, destType, mapType) => destType.IsRecord());

				//typeAdapter.MapToConstructor(true);
				//typeAdapter.IgnoreMember((member, side) => side == MemberSide.Destination && member.Info is PropertyInfo);

				return services;
		}
}
