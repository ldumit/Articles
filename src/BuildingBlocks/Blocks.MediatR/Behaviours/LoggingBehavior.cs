﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Blocks.MediatR.Behaviours;
public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> _logger, IHttpContextAccessor _httpContextAccessor)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
				var correlationId = _httpContextAccessor.HttpContext?.Items["X-Correlation-ID"]?.ToString();

				_logger.LogDebug("[Begin] Handling {Request} | CorrelationId: {CorrelationId}", typeof(TRequest).Name, correlationId);

				var stopwatch = Stopwatch.StartNew();
				var response = await next();
				stopwatch.Stop();
				var requestDuration = stopwatch.Elapsed;

				var httpRequest = _httpContextAccessor.HttpContext?.Request;
				// identifying upload/download requests
				var isFileTransfer = httpRequest?.ContentType?.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase) == true
														 || httpRequest?.Path.ToString().Contains("download", StringComparison.OrdinalIgnoreCase) == true;

				var thresholdMs = isFileTransfer ? 3000 : 1000;
				if (requestDuration.TotalMilliseconds > thresholdMs)
				{
						_logger.LogWarning("[PerfWarn] {Request} took {Elapsed} ms. (FileTransfer: {IsFileTransfer})",
								typeof(TRequest).Name, (int)requestDuration.TotalMilliseconds, isFileTransfer);
				}

				_logger.LogDebug("End handling {Request} in {Elapsed}ms | CorrelationId: {CorrelationId}", typeof(TRequest).Name, stopwatch.ElapsedMilliseconds, correlationId);
				return response;
    }
}

