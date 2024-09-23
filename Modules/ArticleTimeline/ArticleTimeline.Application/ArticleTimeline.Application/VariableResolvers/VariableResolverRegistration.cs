using Microsoft.Extensions.DependencyInjection;

namespace ArticleTimeline.Application.VariableResolvers;

public delegate IVariableResolver VariableResolverFactory(VariableResolverType resolverType);

public static class VariableResolverRegistration
{
    public static IServiceCollection AddArticleTimelineVariableResolvers(this IServiceCollection services)
    {
        services.AddScoped<CurrentUserRoleResolver>();
        services.AddScoped<UploadedFileResolver>();
        services.AddScoped<ArticleStageFileResolver>();
        services.AddScoped<UserNameFileResolver>();
        services.AddScoped<MessageResolver>();
        services.AddScoped<SubmittedUserNameResolver>();
        services.AddScoped<SubmittedUserRoleResolver>();

        services.AddScoped<VariableResolverFactory>(serviceProvider => variableType =>
        {
            return variableType switch
            {
                VariableResolverType.RoleUser => serviceProvider.GetService<CurrentUserRoleResolver>(),
                VariableResolverType.UploadedFile => serviceProvider.GetService<UploadedFileResolver>(),
                VariableResolverType.ArticleStage => serviceProvider.GetService<ArticleStageFileResolver>(),
                VariableResolverType.UserName => serviceProvider.GetService<UserNameFileResolver>(),
                VariableResolverType.Message => serviceProvider.GetService<MessageResolver>(),
                VariableResolverType.SubmittedUserName => serviceProvider.GetService<SubmittedUserNameResolver>(),
                VariableResolverType.SubmittedUserRole => serviceProvider.GetService<SubmittedUserRoleResolver>(),

                _ => throw new ApplicationException(),
            };
        });
        return services;
    }

}
