﻿using Auth.Grpc;
using Submission.Application.Dtos;

namespace Submission.Application.Mappings;

public class ApplicationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ArticleActor, ContributorDto>()
                .Include<ArticleAuthor, ContributorDto>();

        config.NewConfig<Person, PersonDto>()
                .Include<Author, PersonDto>();

        config.NewConfig<IArticleAction<ArticleActionType>, ArticleAction>()
                .Map(dest => dest.TypeId, src => src.ActionType);

				config.ForType<string, EmailAddress>()
						.MapWith(src => EmailAddress.Create(src));

				config.ForType<UserInfo, Author>()
						.Map(dest => dest.UserId, src => src.Id)
						.Ignore(dest => dest.Id); 
		}
}
