﻿using Auth.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Models;
using EmailService.Contracts;
using EmailService.Smtp;
using Articles.Security;
using Blocks.Security;

namespace Auth.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
				services.AddDbContext<AuthDBContext>(opts => opts.UseSqlServer(connectionString));


				///services.AddScoped<UserManager<User>>();
				//services.AddScoped<SignInManager<User>>();
				services.AddScoped<IEmailService, SmtpEmailService>();
				services.AddScoped<TokenFactory>();


				return services;
    }

		//public static void AddSqlServerDbContext<TDbContext>(this IServiceCollection services, string connectionString)
		//		where TDbContext : DbContext
		//{
		//		if (string.IsNullOrEmpty(connectionString))
		//		{
		//				throw new ArgumentException(nameof(connectionString));
		//		}
		//		services.AddDbContext<TDbContext>(opts => opts.UseSqlServer(connectionString));
		//}
}
