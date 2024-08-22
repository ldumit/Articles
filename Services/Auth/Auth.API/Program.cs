using Auth.API;
using Articles.AspNetCore.Dependencies;
using FastEndpoints;
using Auth.Application;
using Articles.EntityFrameworkCore;
using Auth.Persistence;
using System.Reflection;
using EmailService.SendGrid;
using EmailService.Contracts;
using Articles.AspNetCore;
using GraphQL;
using GraphQL.Types;
using Auth.Application.GraphQLSchemas;
using GraphQL.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

builder.Services
		.ConfigureOptions<EmailOptions>(builder.Configuration)
		.ConfigureOptions<SendGridAccountOptions>(builder.Configuration)
		.ConfigureOptions<JwtOptions>(builder.Configuration)
		.ConfigureOptions<PostConfigureJwtBearerOptions>();


builder.Services
		.AddFastEndpoints()
		.AddEndpointsApiExplorer()
		.AddAutoMapper(new Assembly[] {typeof(Auth.API.Features.CreateUserCommandMapping).Assembly})
		.AddApplicationServices(builder.Configuration)
    .AddSwaggerGen()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorization()
    .AddJwtIdentity(builder.Configuration);

builder.Services.AddGraphQL(builder =>
		{
				builder
				.AddNewtonsoftJson()
				.AddSchema<UserSchema>();
		});

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
app.UseFastEndpoints();

app.UseGraphQL<UserSchema>("/graphql/user");
app.UseGraphQLGraphiQL();

app
		.UseRouting()
		.UseEndpoints(endpoints =>
		{
				endpoints.MapGraphQL("/graphql/user");
		});

//app.MapControllers();

app.Run();