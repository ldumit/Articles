using ArticleHub.API;
using ArticleHub.Application;
using ArticleHub.Persistence;
using Blocks.AspNetCore;
using Blocks.AspNetCore.Middlewares;
using Blocks.EntityFrameworkCore;
using Carter;

var builder = WebApplication.CreateBuilder(args);

#region Add
builder.Services
		.ConfigureOptions(builder.Configuration);        // Configure Options

builder.Services
		.AddApiServices(builder.Configuration)              // Register API/Infra-specific services
		.AddApplicationServices(builder.Configuration)      // Register Application-specific services
		.AddPersistenceServices(builder.Configuration);     // Register Persistence-specific services
#endregion


var app = builder.Build();

#region Use
app.MapCarter();

app
		.UseSwagger()
		.UseSwaggerUI()
		.UseRouting()
		.UseAuthentication()
		.UseAuthorization()
		.UseMiddleware<GlobalExceptionMiddleware>()
		.UseMiddleware<RequestContextMiddleware>();

#endregion

#region InitData
app.Migrate<ArticleHubDbContext>();

if (app.Environment.IsDevelopment())
{
		//app.SeedTestData();
}
#endregion

app.Run();
