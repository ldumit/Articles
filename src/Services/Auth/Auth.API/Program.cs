using Auth.Application;
using Auth.Persistence;
using Blocks.AspNetCore;
using System.Text.Json.Serialization;
using Auth.API.Features.GetUserInfo;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

#region Add
builder.Services
		.ConfigureApiOptions(builder.Configuration); // Configure Options

builder.Services
		.AddApiServices(builder.Configuration)     // Register API-specific services
		.AddPersistenceServices(builder.Configuration);
#endregion

var app = builder.Build();

#region InitData
app.Migrate<AuthDBContext>();
if (app.Environment.IsDevelopment())
{
		//app.SeedTestData();
}
#endregion

#region Use
app
		.UseSwagger()
		.UseSwaggerUI()
		.UseHttpsRedirection()
		.UseMiddleware<GlobalExceptionMiddleware>()
		.UseAuthentication()
		.UseRouting()
		.UseAuthorization()
		.UseEndpoints(endpoints =>
		{
				endpoints.MapControllers(); // required if using MVC
				endpoints.MapDefaultControllerRoute(); // optional
		})
		.UseFastEndpoints(config =>
		{
				config.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
				//config..Add(new AssignUserIdPreProcessor(app.Services.GetRequiredService<IHttpContextAccessor>()));
		})
		.UseSwaggerGen();

app.MapGrpcService<GetUserInfoGrpc>();
#endregion

app.Run();