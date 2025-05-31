using Auth.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
				services.AddDbContext<AuthDBContext>(opts => opts.UseSqlServer(connectionString));

				return services;
    }
}
