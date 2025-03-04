var builder = WebApplication.CreateBuilder(args);

//todo add authentication

builder.Services.AddReverseProxy()
		.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();


app.MapReverseProxy();

app.Run();