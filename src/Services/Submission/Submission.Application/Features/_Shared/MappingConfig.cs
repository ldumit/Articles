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
				config.NewConfig<ArticleActor, ActorDto>()
						.Include<AuthorActor, ActorDto>();

				config.NewConfig<Person, PersonDto>()
						.Include<Author, PersonDto>();

				config.NewConfig<IArticleAction<AssetActionType>, AssetAction>()
						.Map(dest => dest.TypeId, src => src.ActionType);
		}
}
