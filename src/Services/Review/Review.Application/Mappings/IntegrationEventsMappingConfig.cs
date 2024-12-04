using Articles.Abstractions.Events.Dtos;

namespace Review.Application.Mappings;

public class IntegrationEventsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ArticleContributor, ContributorDto>()
                .Include<ArticleAuthor, ContributorDto>();

        config.NewConfig<Person, PersonDto>()
                .Include<Author, PersonDto>();

        config.NewConfig<IArticleAction<ArticleActionType>, ArticleAction>()
                .Map(dest => dest.TypeId, src => src.ActionType);
    }
}
