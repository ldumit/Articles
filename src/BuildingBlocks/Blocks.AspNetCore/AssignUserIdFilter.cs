using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using Blocks.Domain;
using Blocks.Core.Security;

namespace Blocks.AspNetCore;

//talk - cannot use filters with Minimal API or FastEnppoints
public class AssignUserIdFilter(IClaimsProvider _claimsProvider) : IActionFilter
{
		public void OnActionExecuting(ActionExecutingContext context)
		{
				if (context.ActionArguments.TryGetValue("command", out var commandObj) && commandObj is IAuditableAction command)
				{
						var userId = _claimsProvider.GetUserId();

						var userIdProperty = typeof(IAuditableAction).GetProperty("UserId", BindingFlags.NonPublic | BindingFlags.Instance);
						userIdProperty?.SetValue(command, userId);
				}
		}

		public void OnActionExecuted(ActionExecutedContext context) { }
}
