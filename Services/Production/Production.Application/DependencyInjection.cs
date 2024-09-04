using Articles.AspNetCore;
using Articles.Security;
using Articles.System;
using FileStorage.AzureBlob;
using FileStorage.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Production.Persistence.Repositories;

namespace Production.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        //services.AddMediatR(config =>
        //{
        //    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        //    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        //});

        //services.AddFeatureManagement();
        //services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        services.AddDbContext<Production.Persistence.ProductionDbContext>((sp, options) =>
        {
            //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });


				services.AddScoped<IAuthorizationHandler, ArticleRoleAuthorizationHandler>();

				services.AddScoped<ClaimsProvider>();

        //talk - SOLID principle interface segragation, injecting multiple interfaces using the same class
				services.AddScoped<IClaimsProvider, HttpContextProvider>(); 
        services.AddScoped<IRouteProvider, HttpContextProvider>();
        services.AddScoped<HttpContextProvider>();

        services.AddScoped<IArticleRoleChecker, ActorRepository>();
				services.AddScoped<ArticleRepository>();
				services.AddScoped<AssetRepository>(); 

				services.AddScoped<IThreadSafeMemoryCache, MemoryCache>();
				services.AddScoped<IFileService, FileService>();


				return services;
    }
}
