using Auth.Grpc;

namespace Auth.API.Mappings;

public class GrpcTypeAdapterConfig : TypeAdapterConfig
{
		public GrpcTypeAdapterConfig()
		{
				this.ForType<string?, string>()
						.MapWith(src => src ?? string.Empty);

				this.NewConfig<Person, PersonInfo>()
						//.Ignore(dest => dest.Honorific)
						.IgnoreNullValues(true);
						

				//this.NewConfig<Articles.Abstractions.Enums.Gender, Gender>()
				//		.MapWith(src => (Gender)src);

				//this.NewConfig<Articles.Abstractions.Enums.Honorific, Honorific>()
				//		.MapWith(src => (Honorific)src);

				this.NewConfig<Auth.Domain.Persons.ValueObjects.ProfessionalProfile, ProfessionalProfile>();
		}
}
