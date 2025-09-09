using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Blocks.AspNetCore.Middlewares;

public sealed class ResponseTimingMiddleware(RequestDelegate next, ILogger<ResponseTimingMiddleware> logger)
{
		public async Task InvokeAsync(HttpContext ctx)
		{
				var sw = Stopwatch.StartNew();

				// Stamp headers *right before* the response is sent (works even if body already started)
				ctx.Response.OnStarting(() =>
				{
						var elapsedMs = sw.Elapsed.TotalMilliseconds;
						ctx.Response.Headers["X-Elapsed-Ms"] = ((long)elapsedMs).ToString(CultureInfo.InvariantCulture);

						// If you already have a correlation id, echo it (adjust header/key to your middleware)
						var cid = ctx.TraceIdentifier; // or ctx.Items["CorrelationId"] / ctx.Request.Headers["X-Correlation-Id"]
						if (!string.IsNullOrWhiteSpace(cid))
								ctx.Response.Headers["X-Correlation-Id"] = cid;

						return Task.CompletedTask;
				});

				try
				{
						await next(ctx);
				}
				finally
				{
						sw.Stop();
						logger.LogInformation("HTTP {Method} {Path} -> {Status} in {ElapsedMs} ms (cid={CorrelationId})",
								ctx.Request.Method,
								ctx.Request.Path.Value,
								ctx.Response?.StatusCode,
								sw.Elapsed.TotalMilliseconds,
								ctx.TraceIdentifier);
				}
		}
}
