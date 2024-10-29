using Articles.Abstractions;
using Articles.AspNetCore;
using MediatR;

namespace Articles.MediatR.Behaviours;

public class SetUserIdBehavior<TRequest, TResponse>
		(IClaimsProvider _claimsProvider) 
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : IAction
{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
				if (request is IAction action)
						action.CreatedById = _claimsProvider.GetUserId();

				return await next();
		}
}