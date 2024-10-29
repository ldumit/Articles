using Articles.Abstractions;
using Mapster;
using Submission.Application.Dtos;
using Submission.Domain.Entities;
using Submission.Domain.Enums;

namespace Submission.Application.Features.Shared;

public class MappingConfig : IRegister
{
		public void Register(TypeAdapterConfig config)
		{
				config.NewConfig<Asset, AssetDto>()
						.Map(dest => dest.Name, src => src.Name.Value)
						.Map(dest => dest.Number, src => src.Number.Value);


				config.NewConfig<Domain.Entities.File, FileDto>()
						.Map(dest => dest.Name, src => src.Name.Value);
						//.Map(dest => dest.Version, src => src.Version.Value);

				config.NewConfig<Article, ArticleDto>();
				config.NewConfig<ArticleActor, ActorDto>();
				config.NewConfig<Person, PersonDto>();


				config.NewConfig<IArticleAction<AssetActionType>, AssetAction>()
						.Map(dest => dest.TypeId, src => src.ActionType);
		}
}
