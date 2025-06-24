using Submission.Application;
using Blocks.EntityFrameworkCore;
using Submission.Persistence;
using Blocks.AspNetCore;
using Submission.API.Endpoints;
using Submission.API;
using Submission.Persistence.Data.Test;
using Blocks.AspNetCore.Middlewares;

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

#region InitData
//talk - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<SubmissionDbContext>();
if (app.Environment.IsDevelopment())
{
		app.Services.SeedTestData();
}
#endregion

#region Use
app
		.UseSwagger()
		.UseSwaggerUI()
		.UseRouting()                                       // match the HTTP request to an endpoint (route) based on the URL
		.UseMiddleware<GlobalExceptionMiddleware>()
		.UseMiddleware<CorrelationIdMiddleware>()
		.UseAuthentication()
		.UseAuthorization();

app.MapAllEndpoints();
#endregion

app.Run();
