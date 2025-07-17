using FastEndpoints.Swagger;
using Blocks.AspNetCore;
using Journals.API;
using Journals.Persistence;
using Journals.Persistence.Data.Test;
using Blocks.FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services
		.ConfigureApiOptions(builder.Configuration);				// Configure Options

builder.Services
		.AddApiServices(builder.Configuration)              // Register API-specific services
		.AddPersistenceServices(builder.Configuration);     // Register Persistence-specific services
#endregion

var app = builder.Build();

#region InitData
if (app.Environment.IsDevelopment())
{
		await app.SeedTestData();
}
#endregion

#region Use
app
		.UseSwagger()
		.UseSwaggerUI()

		.UseRedis()
		
		.UseHttpsRedirection()
		
		.UseMiddleware<GlobalExceptionMiddleware>()
		
		.UseRouting()
		.UseAuthentication()
		.UseAuthorization()
		//.UseEndpoints(endpoints =>
		//{
		//		endpoints.MapControllers();
		//		endpoints.MapDefaultControllerRoute();

		//})

		.UseCustomFastEndpoints()
		.UseSwaggerGen();
#endregion

app.Run();