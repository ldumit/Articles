using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Blocks.AspNetCore.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate _next, ILogger<CorrelationIdMiddleware> _logger)
{
		private const string Header = "X-Correlation-ID";

		public async Task InvokeAsync(HttpContext context)
		{
				var correlationId = context.Request.Headers.TryGetValue(Header, out var value)
						? value.ToString()
						: Guid.NewGuid().ToString();

				context.Items[Header] = correlationId;

				using (_logger.BeginScope(new Dictionary<string, object>
				{
						[Header] = correlationId
				}))
				{
						await _next(context);
				}
		}
}

