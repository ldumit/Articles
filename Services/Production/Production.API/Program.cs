using Articles.AspNetCore.Dependencies;
using FastEndpoints;
using Production.Application;

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


// Configure the HTTP request pipeline.
app.Migrate();
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
