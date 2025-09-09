using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Blocks.AspNetCore.Middleware;

public sealed class RequestLoggingMiddleware(RequestDelegate _next, ILogger<RequestLoggingMiddleware> _logger)
{		
		public async Task InvokeAsync(HttpContext ctx)
		{
				// Correlation
				var correlationId = ctx.Items["X-Correlation-ID"] as string ?? ctx.TraceIdentifier;

				// Begin
				_logger.LogDebug("[Begin] HTTP {Method} {Path} | CorrelationId: {CorrelationId}",
						ctx.Request.Method, ctx.Request.Path, correlationId);

				var sw = Stopwatch.StartNew();
				await _next(ctx);
				sw.Stop();

				var elapsedMs = sw.ElapsedMilliseconds;

				// Heuristic: upload/download
				var isFileTransfer =
						(ctx.Request.ContentType?.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase) ?? false)
						|| (ctx.Request.Path.ToString().Contains("download", StringComparison.OrdinalIgnoreCase));

				var thresholdMs = isFileTransfer ? 3000 : 1000;
				if (elapsedMs > thresholdMs)
				{
						_logger.LogWarning("[PerfWarn] {Method} {Path} took {Elapsed} ms. (FileTransfer: {IsFileTransfer})",
								ctx.Request.Method, ctx.Request.Path, elapsedMs, isFileTransfer);
				}

				// End
				_logger.LogDebug("[End] HTTP {Method} {Path} -> {Status} in {Elapsed}ms | CorrelationId: {CorrelationId}",
						ctx.Request.Method, ctx.Request.Path, ctx.Response.StatusCode, elapsedMs, correlationId);
		}
}

