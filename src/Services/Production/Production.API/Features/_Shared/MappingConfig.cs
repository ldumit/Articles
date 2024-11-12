using Articles.Abstractions;
using Mapster;
using Production.Application.Dtos;
using Production.Domain.Entities;
using Production.Domain.Enums;

namespace Production.API.Features.Shared;

public class MappingConfig : IRegister
{
		public void Register(TypeAdapterConfig config)
		{
				config.NewConfig<Asset, AssetMinimalDto>()
						.Map(dest => dest.Id, src => src.Id)
						.Map(dest => dest.File, src => src.CurrentFile);

				config.NewConfig<Domain.Entities.File, FileMinimalDto>()
						.Map(dest => dest.Version, src => src.Version.Value);

				config.NewConfig<Asset, AssetDto>()
						.Map(dest => dest.Name, src => src.Name.Value)
						.Map(dest => dest.Number, src => src.Number.Value);


				config.NewConfig<Domain.Entities.File, FileDto>()
						.Map(dest => dest.Name, src => src.Name.Value)
						.Map(dest => dest.Version, src => src.Version.Value);

				config.NewConfig<Article, ArticleSummaryDto>();
				config.NewConfig<ArticleContributor, ContributorDto>();
				config.NewConfig<Person, PersonDto>();


				config.NewConfig<IArticleAction<AssetActionType>, AssetAction>()
						.Map(dest => dest.TypeId, src => src.ActionType);
		}
}
