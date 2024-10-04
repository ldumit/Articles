using Articles.Abstractions;
using Mapster;
using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.API.Features.Shared;

public class MappingConfig : IRegister
{
		public void Register(TypeAdapterConfig config)
		{
				config.NewConfig<Asset, AssetActionResponse>()
						.Map(dest => dest.File, src => src.CurrentFile);

				config.NewConfig<Domain.Entities.File, FileDto>()
						.Map(dest => dest.FileId, src => src.Id)
						.Map(dest => dest.Version, src => src.Version.Value);


				config.NewConfig<IArticleAction<AssetActionType>, AssetAction>()
						.Map(dest => dest.TypeId, src => src.ActionType);
		}
}
