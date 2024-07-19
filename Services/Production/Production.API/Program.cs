using FastEndpoints;
//using Ordering.Application;
using Production.Application;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//talk - fluid vs normal
builder.Services
    .AddFastEndpoints()
    .AddEndpointsApiExplorer()
    .AddMemoryCache()
    .AddApplicationServices(builder.Configuration)
    .AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

Console.WriteLine($"Launched from {Environment.CurrentDirectory}");
Console.WriteLine($"Physical location {AppDomain.CurrentDomain.BaseDirectory}");
Console.WriteLine($"AppContext.BaseDir {AppContext.BaseDirectory}");
Console.WriteLine($"Runtime Call {Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)}");

// Configure the HTTP request pipeline.
app.Migrate();
if (app.Environment.IsDevelopment())
{
    app.SeedTestData();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.UseFastEndpoints();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
