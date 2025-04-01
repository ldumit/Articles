using Microsoft.Extensions.DependencyInjection;
using Polly;
using Grpc.Core;

namespace Blocks.AspNetCore.Grpc;

public static class GrpcClientRegistrationExtensions
{
		public static IHttpClientBuilder AddConfiguredGrpcClient<TClient>(this IServiceCollection services, GrpcServicesOptions grpcOptions, string? serviceKey = null)
				where TClient : class
		{
				//serviceKey ??= typeof(TClient).Namespace?.Split('.').FirstOrDefault()?.ToLowerInvariant();
				serviceKey ??= typeof(TClient).Name.Replace("Client", "").Replace("Service", "");

				if (string.IsNullOrWhiteSpace(serviceKey) || !grpcOptions.Services.TryGetValue(serviceKey, out var serviceSettings))
				{
						throw new InvalidOperationException($"Missing GrpcService config for: {typeof(TClient).Name}");
				}

				var clientBuilder = services.AddGrpcClient<TClient>(o =>
				{
						o.Address = new Uri(serviceSettings.Url);
				});

#if DEBUG
				// Accept self-signed certs in dev only
				clientBuilder.ConfigurePrimaryHttpMessageHandler(() =>
						new HttpClientHandler
						{
								ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
						});
#endif

				if (serviceSettings.EnableRetry == true)
				{
						clientBuilder.AddPolicyHandler(Policy<HttpResponseMessage>
								.Handle<RpcException>()
								.WaitAndRetryAsync(
										grpcOptions.Retry.Count,
										retryAttempt => TimeSpan.FromMilliseconds(grpcOptions.Retry.InitialDelayMs * retryAttempt)
								));
				}

				return clientBuilder;
		}
}
