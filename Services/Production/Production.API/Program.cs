using Articles.AspNetCore.Dependencies;
using FastEndpoints;
using Production.Application;
using Articles.EntityFrameworkCore;
using Production.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Add
//talk - fluid vs normal
builder.Services
    .AddFastEndpoints()
    .AddEndpointsApiExplorer()
    .AddMemoryCache()
    .AddApplicationServices(builder.Configuration)
    .AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization();
#endregion

var app = builder.Build();

#region Use

app.UseSwagger();
app.UseSwaggerUI();


//talk - explain when is the best time to run the migration, integrate the migration in the CI pipeline
app.Migrate<DbContext>();
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
