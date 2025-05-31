using Production.Application;
using Blocks.EntityFrameworkCore;
using Production.Persistence;
using Azure.Storage.Blobs;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http.Json;
using Blocks.FastEnpoints;
using Blocks.Core;
using System.Reflection;
using ArticleTimeline.Persistence;
using ArticleTimeline.Application;

var builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services
		//.ConfigureOptions<FileStorage.Contracts.FileServerOptions>(builder.Configuration)
		.ConfigureOptionsFromSection<TransactionOptions>(builder.Configuration)
		.Configure<JsonOptions>(opt =>
		{
				opt.SerializerOptions.PropertyNameCaseInsensitive = true;
				opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});


//talk - fluid vs normal
builder.Services.AddControllers();

//builder.Services.AddScoped<IValidator<AssignTypesetterCommand>, AssignTypesetterCommandValidator>();

builder.Services
    .AddMemoryCache()
		.AddFastEndpoints()
    .AddMapster()
    .SwaggerDocument()
    .AddEndpointsApiExplorer()
		.AddAutoMapper(new Assembly[] { typeof(Production.API.Features.Shared.FileResponseMappingProfile).Assembly })
		.AddDistributedMemoryCache() //.AddMemoryCache()
    .AddApplicationServices(builder.Configuration)
		.AddTimelineApplicationServices(builder.Configuration)
		.AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization()
		;


//overides the singleton lifetime for validators done by FastEndpoints
//builder.Services.Scan(scan => scan
//		.FromAssemblyOf<Program>()
//		.AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
//		.AsImplementedInterfaces()
//		.WithScopedLifetime());

//builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("FileServer")));
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
