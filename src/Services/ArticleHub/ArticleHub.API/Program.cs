using Blocks.AspNetCore;
using ArticleHub.Persistence;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Articles.Security;
using Blocks.Core;
using Blocks.EntityFrameworkCore;
using Blocks.Messaging;
using ArticleHub.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
		.ConfigureOptions<HasuraOptions>(builder.Configuration)
		.ConfigureOptions<RabbitMqOptions>(builder.Configuration)
		.Configure<JsonOptions>(opt =>
		{
				opt.SerializerOptions.PropertyNameCaseInsensitive = true;
				opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});

builder.Services
		.AddMemoryCache()
		.AddHttpContextAccessor()
		.AddEndpointsApiExplorer()
		.AddSwaggerGen()
		.AddApplicationServices(builder.Configuration)
		.AddPersistenceServices(builder.Configuration)
		.AddJwtAuthentication(builder.Configuration)
		.AddAuthorization()                            // Authorization immediately after authentication
		.AddEndpointsApiExplorer()                     // Minimal api for Swagger
		.AddSwaggerGen();


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
