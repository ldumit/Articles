using Auth.API;
using Articles.AspNetCore.Dependencies;
using FastEndpoints;
using Auth.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions<ConfigureJwtOptions>();
builder.Services.ConfigureOptions<ConfigureJwtBearerOptions>();

builder.Services
		.AddFastEndpoints()
		.AddEndpointsApiExplorer()
		.AddApplicationServices(builder.Configuration)
    .AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddJwtIdentity(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//use
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

//app.MapControllers();

app.Run();