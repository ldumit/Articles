using Review.Application;
using Blocks.EntityFrameworkCore;
using Review.Persistence;
using Blocks.AspNetCore;
using Review.API.Endpoints;
using Review.API;
using Review.Persistence.Data.Test;

var builder = WebApplication.CreateBuilder(args);

#region Add
builder.Services
		.ConfigureApiOptions(builder.Configuration);				// Configure Options

builder.Services
		.AddApiServices(builder.Configuration)							// Register API-specific services
		.AddApplicationServices(builder.Configuration)			// Register application-specific services (e.g., services for business logic)
		.AddPersistenceServices(builder.Configuration);
#endregion

var app = builder.Build();

#region Use
app
		.UseSwagger()
		.UseSwaggerUI()
		.UseRouting()
		.UseAuthentication()
		.UseAuthorization()
		.UseMiddleware<GlobalExceptionMiddleware>();

app.MapAllEndpoints();

//talk - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<ReviewDbContext>();
//todo - integrate ArticleTimeline with domain events
//app.Migrate<ArticleTimelineDbContext>();
if (app.Environment.IsDevelopment())
{
		app.SeedTestData();
}
#endregion

app.Run();
