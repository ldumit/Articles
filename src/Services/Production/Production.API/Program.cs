using Production.API;
using Production.Application;
using Blocks.EntityFrameworkCore;
using Production.Persistence;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Blocks.FastEnpoints;
using ArticleTimeline.Persistence;
using ArticleTimeline.Application;
using FileStorage.AzureBlob;

var builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services
		.ConfigureApiOptions(builder.Configuration);        // Configure Options

// Microservice layers
builder.Services
		.AddApiServices(builder.Configuration)
		.AddApplicationServices(builder.Configuration)
		.AddPersistenceServices(builder.Configuration);

// Shared modules
builder.Services
		.AddArticleTimeline(builder.Configuration)
		.AddAzureFileStorage(builder.Configuration);

#endregion

var app = builder.Build();

#region Use
//app.UseMiddleware<AssignUserIdMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();


//talk - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<ProductionDbContext>();
app.Migrate<ArticleTimelineDbContext>();
if (app.Environment.IsDevelopment())
{
    app.SeedTestData();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
		endpoints.MapDefaultControllerRoute();

});
app
    .UseFastEndpoints(config =>
    {
		    config.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
		    config.Endpoints.Configurator = ep =>
		    {
				    ep.PreProcessor<AssignUserIdPreProcessor>(FastEndpoints.Order.Before);
		    };
    })
		.UseSwaggerGen();

#endregion

app.Run();
