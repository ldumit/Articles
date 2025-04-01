using Auth.API;
using Articles.Security;
using FastEndpoints;
using Auth.Application;
using Blocks.EntityFrameworkCore;
using Auth.Persistence;
using System.Reflection;
using EmailService.Contracts;
using Blocks.AspNetCore;
using Blocks.Core;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Auth.API.Features.GetUserInfo;

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


builder.Services.AddGrpc();

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

app.Migrate<AuthDBContext>();
if (app.Environment.IsDevelopment())
{
		app.SeedTestData();
}

//use
app
		.UseHttpsRedirection()
		.UseAuthentication()
		.UseAuthorization()
		.UseMiddleware<GlobalExceptionMiddleware>();

app.UseFastEndpoints(config =>
{
		config.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
		//config..Add(new AssignUserIdPreProcessor(app.Services.GetRequiredService<IHttpContextAccessor>()));
});

app.MapGrpcService<GetUserInfoGrpc>();

//app.MapControllers();

app.Run();