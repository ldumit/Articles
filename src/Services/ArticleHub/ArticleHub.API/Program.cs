using Carter;
using Blocks.AspNetCore;
using Blocks.EntityFrameworkCore;
using Blocks.AspNetCore.Middlewares;
using ArticleHub.Persistence;
using ArticleHub.Application;
using ArticleHub.API;

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

app.MapCarter();

app
		.UseSwagger()
		.UseSwaggerUI()
		.UseRouting()
		.UseAuthentication()
		.UseAuthorization()
		.UseMiddleware<GlobalExceptionMiddleware>()
		.UseMiddleware<CorrelationIdMiddleware>();

app.Migrate<ArticleHubDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
		//app.SeedTestData();
}

app.Run();
