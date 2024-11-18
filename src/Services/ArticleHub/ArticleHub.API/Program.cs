using Blocks.AspNetCore;
using ArticleHub.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
		.ConfigureOptions<HasuraOptions>(builder.Configuration);

builder.Services
		.AddEndpointsApiExplorer()
		.AddSwaggerGen()
		.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
		app.UseSwagger();
		app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
