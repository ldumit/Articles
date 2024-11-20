using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;
using FluentValidation;
using Blocks.Domain;
using Blocks.Exceptions;

namespace Blocks.AspNetCore;

public class GlobalExceptionMiddleware(RequestDelegate _next)
{
		public async Task InvokeAsync(HttpContext context)
		{
				try
				{
						await _next(context);
				}
				catch (NotFoundException ex)
				{
						await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
				}
				catch (BadRequestException ex)
				{
						await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
				}
				catch (ValidationException ex)
				{
						await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
				}
				catch (DomainException ex)
				{
						await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
				}
				catch (ArgumentException ex)
				{
						await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
				}
				catch (Exception ex)
				{
						await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
				}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
		{
				context.Response.StatusCode = (int)statusCode;
				context.Response.ContentType = "application/json";

				var response = new
				{
						context.Response.StatusCode,
						exception.Message,
						Details = exception.StackTrace // todo - in production we shouldn't expose the stack trace
				};

				var responseJson = JsonConvert.SerializeObject(response);
				return context.Response.WriteAsync(responseJson);
		}
}
