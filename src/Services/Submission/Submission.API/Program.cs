using Articles.Security;
using Submission.Application;
using Articles.EntityFrameworkCore;
using Submission.Persistence;
using Articles.AspNetCore;
using Azure.Storage.Blobs;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using ArticleTimeline.Persistence;
using ArticleTimeline.Application;
using Submission.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

#region Add
builder.Services
		.ConfigureOptions<FileStorage.Contracts.FileServerOptions>(builder.Configuration)
		.ConfigureOptions<TransactionOptions>(builder.Configuration)
		.Configure<JsonOptions>(opt =>
		{
				opt.SerializerOptions.PropertyNameCaseInsensitive = true;
				opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});


//talk - fluid vs normal
builder.Services
		.AddMemoryCache()
		.AddMapster()
		.AddHttpContextAccessor()
		.AddApplicationServices(builder.Configuration)  // Register application-specific services first
		.AddJwtAuthentication(builder.Configuration)
		.AddAuthorization()                            // Authorization immediately after authentication
		.AddEndpointsApiExplorer()                     // Minimal api for Swagger
		.AddSwaggerGen()
		//todo - decide how to split the required services between the projects
		//.AddMediatR(config =>
		//{
		//		config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
		//		config.AddOpenBehavior(typeof(SetUserIdBehavior<,>));
		//		//config.AddOpenBehavior(typeof(ValidationBehavior<,>));
		//		//config.AddOpenBehavior(typeof(LoggingBehavior<,>));
		//})
		.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("FileServer")));
#endregion

var app = builder.Build();

#region Use

app
		.UseSwagger()
		.UseSwaggerUI()
		.UseRouting()
		.UseAuthentication()
		.UseAuthorization();

app.MapAllEndpoints();

//talk - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<SubmissionDbContext>();
//todo - integrate ArticleTimeline with domain events
//app.Migrate<ArticleTimelineDbContext>();
if (app.Environment.IsDevelopment())
{
		app.SeedTestData();
}
#endregion

app.Run();
