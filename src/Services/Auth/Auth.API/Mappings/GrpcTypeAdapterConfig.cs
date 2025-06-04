using Auth.Grpc;

namespace Auth.API.Mappings;

public class GrpcTypeAdapterConfig : TypeAdapterConfig
{
		public GrpcTypeAdapterConfig()
		{
				this.ForType<string?, string>()
						.MapWith(src => src ?? string.Empty);

				this.NewConfig<User, UserInfo>();
		}
}
