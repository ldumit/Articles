using Articles.Abstractions;
using Mapster;
using Production.API.Features.UploadFiles.Shared;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.API.Features._Shared;

public class MappingConfig : IRegister
{
		public void Register(TypeAdapterConfig config)
		{
				config.NewConfig<Asset, AssetResponse>()
						.Map(dest => dest.File, src => src.CurrentFile);

				config.NewConfig<Asset, FileResponse>()
						.Map(dest => dest.AssetId, src => src.Id);

				config.NewConfig<Domain.Entities.File, FileResponse>()
						.Map(dest => dest.FileId, src => src.Id);

				config.NewConfig<IArticleAction<AssetActionType>, AssetAction>()
						.Map(dest => dest.TypeId, src => src.ActionType);
		}
}
