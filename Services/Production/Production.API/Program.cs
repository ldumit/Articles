using Articles.AspNetCore.Dependencies;
using FastEndpoints;
using Production.Application;
using Articles.EntityFrameworkCore;
using Production.Persistence;
using Articles.AspNetCore;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

#region Add

builder
    .Services.ConfigureOptions<FileServerOptions>(builder.Configuration);

//talk - fluid vs normal
builder.Services
    .AddFastEndpoints()
    .AddEndpointsApiExplorer()
		.AddDistributedMemoryCache() //.AddMemoryCache()
    .AddApplicationServices(builder.Configuration)
    .AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization();

builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("FileServer")));
#endregion

var app = builder.Build();

#region Use

app.UseSwagger();
app.UseSwaggerUI();


//talk - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<ProductionDbContext>();
if (app.Environment.IsDevelopment())
{
    app.SeedTestData();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

#endregion

app.Run();
