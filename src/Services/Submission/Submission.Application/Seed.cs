using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Submission.Domain.Entities;
using Articles.EntityFrameworkCore;

namespace Submission.Application;

public static class Seed
{
    public static void SeedTestData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<Persistence.SubmissionDbContext>();
        context.SeedTestData();
    }

    public static void SeedTestData(this Persistence.SubmissionDbContext context)
    {
        using var transaction = context.Database.BeginTransaction();

				context.Seed<Person>();

				//context.Seed<User>();
				//context.Seed<Typesetter>();
				context.Seed<Journal>();


				//context.Seed<Article>();
				
        //context.Seed<AssetCurrentFileLink>();

				transaction.Commit();
		}
}
