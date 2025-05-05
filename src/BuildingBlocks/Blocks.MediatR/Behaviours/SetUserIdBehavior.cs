using Blocks.Domain;
using Blocks.AspNetCore;
using MediatR;

namespace Blocks.MediatR.Behaviours;

public class SetUserIdBehavior<TRequest, TResponse>
		(IClaimsProvider _claimsProvider) 
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : IAuditableAction
{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
				if (request is IAuditableAction action)
						action.CreatedById = _claimsProvider.GetUserId();

				return await next();
		}
}