using FastEndpoints;
using FastEndpoints.Swagger;
using Journals.API;
using System.Text.Json.Serialization;
using Articles.Security;
using Articles.Mapster;
using Journals.Persistence.TestData;

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
app.UseSwagger();
app.UseSwaggerUI();
app.UseRedis();
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
})
.UseSwaggerGen();


if (app.Environment.IsDevelopment())
{
		await app.SeedTestData();
}
#endregion

app.Run();