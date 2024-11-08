using Mapster;

namespace Articles.Mapster;

public static class Extensions
{
		/// <summary>
		/// For destination Records, maps the source object to the destination object using the constructor with the most parameters.
		/// </summary>
		public static void MapToConstructor<TSource, TDestination>(this TypeAdapterSetter<TSource, TDestination> typeAdapterSetter)
		{
				typeAdapterSetter.MapToConstructor(typeof(TDestination).GetConstructors().First());
		}
}
