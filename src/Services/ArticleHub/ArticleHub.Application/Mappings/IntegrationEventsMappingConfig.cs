using ArticleHub.Domain;
using Articles.Abstractions;
using Articles.Abstractions.Events.Dtos;
using Mapster;

namespace Submission.Application.Mappings;

public class IntegrationEventsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
				config.NewConfig<ArticleDto, Article>()
						.Ignore(dest => dest.Contributors)
						.Ignore(dest => dest.SubmittedBy)
						.Ignore(dest => dest.Journal);
				//config.NewConfig<ArticleContributor, ContributorDto>();

    //    config.NewConfig<Person, PersonDto>();
    }
}
