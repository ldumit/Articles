using Articles.Abstractions;
using Articles.AspNetCore;
using FastEndpoints;

namespace Articles.FastEnpoints;

public class AssignUserIdPreProcessor : IGlobalPreProcessor
{
		public Task PreProcessAsync(IPreProcessorContext context, CancellationToken ct)
		{
				if (context.Request is IArticleAction articleCommand)
				{
						var claimsProvider = context.HttpContext.Resolve<IClaimsProvider>();
						articleCommand.UserId = claimsProvider.GetUserId();
				}

				return Task.CompletedTask;
		}
}
