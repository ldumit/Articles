using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using Blocks.Domain;

namespace Blocks.AspNetCore;

//talk - cannot use filters with Minimal API or FastEnppoints
public class AssignUserIdFilter(IClaimsProvider _claimsProvider) : IActionFilter
{
		public void OnActionExecuting(ActionExecutingContext context)
		{
				if (context.ActionArguments.TryGetValue("command", out var commandObj) && commandObj is IAction command)
				{
						var userId = _claimsProvider.GetUserId();

						var userIdProperty = typeof(IAction).GetProperty("UserId", BindingFlags.NonPublic | BindingFlags.Instance);
						userIdProperty?.SetValue(command, userId);
				}
		}

		public void OnActionExecuted(ActionExecutedContext context) { }
}
