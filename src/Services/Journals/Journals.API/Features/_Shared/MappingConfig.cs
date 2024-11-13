using Journals.Domain.Entities;
using Mapster;
using Blocks.Mapster;

namespace Journals.API.Features.Shared;

public class MappingConfig : IRegister
{
		public void Register(TypeAdapterConfig config)
		{
				config.NewConfig<Journal, JournalDto>().MapToConstructor();
				config.NewConfig<Section, SectionDto>().MapToConstructor();
		}
}
