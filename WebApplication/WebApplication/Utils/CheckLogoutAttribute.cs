
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
	public sealed class LogoutTokenAttribute : TypeFilterAttribute
	{
		public LogoutTokenAttribute() : base(typeof(LogoutTokenFilter))
		{
		}
	}

	public class LogoutTokenFilter : IAsyncActionFilter
	{
		
		public LogoutTokenFilter()
		{
			


		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (context.Filters.OfType<IAllowAnonymousFilter>().Any())
				await next();
			else
			{
				// execute any code before the action executes
				IActionResult result = AuthorizationCheck(context);

				if (result != null)
					context.Result = result;
				else
					await next();
			}

			// execute any code after the action executes
		}

		private IActionResult AuthorizationCheck(ActionExecutingContext context)
		{
			IActionResult result = null;

			ControllerBase controller = context.Controller as ControllerBase;

		
			if (controller?.User != null)
			{
				if (controller.User.Claims.Count() > 0)
				{
					
				}
			}

			return result;
		}
	}
}
