using FastEndpoints;
using FastEndpoints.Swagger;
using Journals.API;
using Journals.Domain.Entities;
using Redis.OM;
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

using (var scope = app.Services.CreateScope())
{
		var provider = scope.ServiceProvider.GetRequiredService<RedisConnectionProvider>();
		provider.Connection.CreateIndex(typeof(Editor));
		provider.Connection.CreateIndex(typeof(Journal));
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
})
.UseSwaggerGen();


if (app.Environment.IsDevelopment())
{
		app.SeedTestData();
}
#endregion

app.Run();