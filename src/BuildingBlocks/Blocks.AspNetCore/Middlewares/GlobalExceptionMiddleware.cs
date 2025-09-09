using Blocks.Domain;
using Blocks.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Blocks.AspNetCore;

public sealed class GlobalExceptionMiddleware(RequestDelegate _next, ILogger<GlobalExceptionMiddleware> _logger, IWebHostEnvironment _env)
{
		private static HttpStatusCode MapStatusCode(Exception ex) => ex switch
		{
				ValidationException => HttpStatusCode.BadRequest,
				ArgumentException => HttpStatusCode.BadRequest,
				BadRequestException => HttpStatusCode.BadRequest,
				NotFoundException => HttpStatusCode.NotFound,
				DomainException => HttpStatusCode.BadRequest,
				_ => HttpStatusCode.InternalServerError
		};

		public async Task InvokeAsync(HttpContext context)
		{
				try
				{
						await _next(context);
				}
				catch (ValidationException ex)
				{
						await HandleValidationExceptionAsync(context, ex);
				}
				catch (OperationCanceledException) //response is likely abandoned by client
				{						
						if (!context.Response.HasStarted)
								context.Response.StatusCode = 499;
				}
				catch (Exception ex)
				{
						await HandleExceptionAsync(context, ex);
				}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
				var statusCode = MapStatusCode(exception);

				if (statusCode > HttpStatusCode.InternalServerError)
				{
						_logger.LogError(exception, "Unhandled exception. TraceId={TraceId}", context.TraceIdentifier);
				}

				context.Response.StatusCode = (int)statusCode;
				context.Response.ContentType = "application/json";

				var response = new
				{
						context.Response.StatusCode,
						exception.Message,
						TraceId = context.TraceIdentifier,
						// Only expose the stack trace in development
						Details = _env.IsDevelopment() ? exception.StackTrace : null
				};

				var responseJson = JsonSerializer.Serialize(response);
				return context.Response.WriteAsync(responseJson);
		}

		private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
		{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				context.Response.ContentType = "application/json";

				var validationErrors = exception.Errors.Select(e => new
				{
						e.PropertyName,
						e.ErrorMessage
				});

				var response = new
				{
						context.Response.StatusCode,
						Message = "One or more validation errors occurred.",
						TraceId = context.TraceIdentifier,
						Details = _env.IsDevelopment() ? exception.StackTrace : null,
						Errors = validationErrors
				};

				var responseJson = JsonSerializer.Serialize(response);
				return context.Response.WriteAsync(responseJson);
		}
}
