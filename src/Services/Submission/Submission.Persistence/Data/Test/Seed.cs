using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Submission.Domain.Entities;
using Blocks.EntityFrameworkCore;

namespace Submission.Persistence.Data.Test;

public static class Seed
{
    public static void SeedTestData(this IHost host)
    {
        //todo - make this generic using DbContext as a constraint
        using var scope = host.Services.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<SubmissionDbContext>();
        context.SeedTestData();
    }

    public static void SeedTestData(this SubmissionDbContext context)
    {
        using var transaction = context.Database.BeginTransaction();

        context.Seed<Author>();
        context.Seed<Journal>();

        transaction.Commit();
    }
}
