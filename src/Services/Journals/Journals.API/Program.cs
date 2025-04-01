using FastEndpoints;
using FastEndpoints.Swagger;
using Journals.API;
using System.Text.Json.Serialization;
using Articles.Security;
using Blocks.Mapster;
using Journals.Persistence.Data.Test;
using Blocks.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

#region Add
builder.Services.AddControllers();

builder.Services
	.AddFastEndpoints()
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddApplicationServices(builder.Configuration)
	.AddJwtAuthentication(builder.Configuration)
	.AddMapster()
	.AddAuthorization()
	;

#endregion

var app = builder.Build();

#region Use
app.UseSwagger()
		.UseSwaggerUI()
		.UseRedis()
		.UseHttpsRedirection()
		.UseAuthentication()
		.UseRouting()
		.UseAuthorization()
		.UseEndpoints(endpoints =>
		{
				endpoints.MapControllers();
				endpoints.MapDefaultControllerRoute();

		})
		.UseMiddleware<GlobalExceptionMiddleware>();

app
.UseFastEndpoints(config =>
{
		config.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
})
.UseSwaggerGen();


if (app.Environment.IsDevelopment())
{
		await app.SeedTestData();
}
#endregion

app.Run();