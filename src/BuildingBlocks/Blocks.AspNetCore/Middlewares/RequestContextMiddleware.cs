using Blocks.Core.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Blocks.AspNetCore.Middlewares;

public sealed class RequestContextMiddleware(RequestDelegate _next, ILogger<RequestContextMiddleware> _log)
{
		private const string CorrelationIDHeader = "X-Correlation-ID";

		public async Task InvokeAsync(HttpContext ctx, RequestContext rc)
		{
				var correlationId = ResolveCorrelationId(ctx);	

				ctx.Items[CorrelationIDHeader] = correlationId;
				rc.CorrelationId = correlationId;

				rc.IsUpload = (ctx.Request.ContentType?.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase) ?? false);
				rc.IsDownload = ctx.Request.Path.Value?.IndexOf("download", StringComparison.OrdinalIgnoreCase) >= 0;

				// send back the CorrelationId in the response
				ctx.Response.OnStarting(() =>
				{
						if (!ctx.Response.Headers.ContainsKey(CorrelationIDHeader))
								ctx.Response.Headers[CorrelationIDHeader] = rc.CorrelationId ?? string.Empty;
						return Task.CompletedTask;
				});

				// opens a logging scope for the rest of the request,
				// so every log line written inside will be enriched with the X-Correlation-ID.
				using (_log.BeginScope(new Dictionary<string, object> { ["CorrelationId"] = rc.CorrelationId ?? "" }))
				{
						await _next(ctx);
				}
		}

		private static string ResolveCorrelationId(HttpContext ctx)
		{
				// return Correlation-Id if exists in the headers
				if (ctx.Request.Headers.TryGetValue(CorrelationIDHeader, out var value) && !string.IsNullOrWhiteSpace(value))
						return value.ToString();

				// fallback to distributed tracing ID (activityId) if this request has a trace(id) parent
				var activityId = Activity.Current?.TraceId.ToString();
				if (!string.IsNullOrEmpty(activityId))
						return activityId!;

				// fallback to TraceIdentifier (kestrel request id)
				return ctx.TraceIdentifier; // or Guid.NewGuid().ToString();
		}
}
