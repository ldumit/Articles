using Auth.API;
using Articles.Security;
using FastEndpoints;
using Auth.Application;
using Articles.EntityFrameworkCore;
using Auth.Persistence;
using System.Reflection;
//using EmailService.SendGrid;
using EmailService.Contracts;
using Articles.AspNetCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services
		.ConfigureOptions<EmailOptions>(builder.Configuration)
		//.ConfigureOptions<SendGridAccountOptions>(builder.Configuration)
		.ConfigureOptions<JwtOptions>(builder.Configuration)
		.ConfigureOptions<PostConfigureJwtBearerOptions>()
		.Configure<JsonOptions>(opt =>
		{
				opt.SerializerOptions.PropertyNameCaseInsensitive = true;
				opt.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
		});


builder.Services
		.AddFastEndpoints()
		.AddEndpointsApiExplorer()
		.AddAutoMapper(new Assembly[] {typeof(Auth.API.Features.CreateUserCommandMapping).Assembly})
		.AddApplicationServices(builder.Configuration)
    .AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddJwtIdentity(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Migrate<ApplicationDbContext>();
if (app.Environment.IsDevelopment())
{
		app.SeedTestData();
}

//use
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(config =>
{
		config.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
		//config..Add(new AssignUserIdPreProcessor(app.Services.GetRequiredService<IHttpContextAccessor>()));
});

//app.MapControllers();

app.Run();