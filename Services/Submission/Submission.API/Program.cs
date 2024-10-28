using Articles.Security;
using Submission.Application;
using Articles.EntityFrameworkCore;
using Submission.Persistence;
using Articles.AspNetCore;
using Azure.Storage.Blobs;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using System.Reflection;
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
builder.Services.AddControllers();

//builder.Services.AddScoped<IValidator<AssignTypesetterCommand>, AssignTypesetterCommandValidator>();

builder.Services
    .AddMemoryCache()
    .AddMapster()
    .AddEndpointsApiExplorer()
		.AddDistributedMemoryCache() //.AddMemoryCache()
    .AddApplicationServices(builder.Configuration)
		.AddTimelineApplicationServices(builder.Configuration)
		.AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization()
		.AddMediatR(config =>
		{
				config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				//config.AddOpenBehavior(typeof(ValidationBehavior<,>));
				//config.AddOpenBehavior(typeof(LoggingBehavior<,>));
		});


//overides the singleton lifetime for validators done by FastEndpoints
//builder.Services.Scan(scan => scan
//		.FromAssemblyOf<Program>()
//		.AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
//		.AsImplementedInterfaces()
//		.WithScopedLifetime());

builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("FileServer")));
#endregion

var app = builder.Build();

#region Use
//app.UseMiddleware<AssignUserIdMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();


//talk - explain when is the best time to run the migration, integrate the migration in the CI pipeline
//app.Migrate<ProductionDbContext>();
//app.Migrate<ArticleTimelineDbContext>();
//if (app.Environment.IsDevelopment())
//{
//    app.SeedTestData();
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapAllEndpoints();

#endregion

app.Run();
