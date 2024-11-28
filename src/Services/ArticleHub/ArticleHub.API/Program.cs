using Blocks.AspNetCore;
using ArticleHub.Persistence;
using Blocks.EntityFrameworkCore;
using ArticleHub.Application;
using ArticleHub.API;

var builder = WebApplication.CreateBuilder(args);

#region Add
builder.Services
		.ConfigureOptions(builder.Configuration);        // Configure Options

builder.Services
		.AddApiServices(builder.Configuration)              // Register API-specific services
		.AddApplicationServices(builder.Configuration)      // Register Application/Infrastructure-specific services
		.AddPersistenceServices(builder.Configuration);     // Register Persistence-specific services
#endregion


var app = builder.Build();

app
		.UseSwagger()
		.UseSwaggerUI()
		.UseRouting()
		.UseAuthentication()
		.UseAuthorization()
		.UseMiddleware<GlobalExceptionMiddleware>();

app.Migrate<ArticleHubDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
		//app.SeedTestData();
}

app.Run();
