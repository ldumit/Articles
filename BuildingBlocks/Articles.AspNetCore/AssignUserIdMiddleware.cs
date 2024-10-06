using Articles.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text.Json;

namespace Articles.AspNetCore;

//talk - Middleware, filter, preprocesors
//In middleware we can only ionject singleton in the ctor. if we want to use another life time we have to send it as parameter to the Invoke method
public class AssignUserIdMiddleware(RequestDelegate _next)
{
		public async Task InvokeAsync(HttpContext context, IClaimsProvider claimsProvider)
		{
				// Only process if the request is a POST and contains JSON
				if (context.Request.HasJsonContentType())
				{
						context.Request.EnableBuffering();
						var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
						context.Request.Body.Position = 0;

						var requestModel = JsonSerializer.Deserialize<dynamic>(body);

						if (requestModel is IArticleAction articleCommand)
						{
								var userId = claimsProvider.GetUserId();
								var userIdProperty = articleCommand.GetType().GetProperty("UserId", BindingFlags.NonPublic | BindingFlags.Instance);
								userIdProperty?.SetValue(articleCommand, userId);
						}
				}

				// Call the next middleware in the pipeline
				await _next(context);
		}
}
