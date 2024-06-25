using Booking.Application.Services.CurrentUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booking.Application.Filters;

public class AuthFilter(ICurrentUserService _currentUserService) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(context.HttpContext.User.Identity.IsAuthenticated is true)
        {
            var user = await _currentUserService.GetUserAsync();
            var roles = await _currentUserService.GetCurrentUserRolesAsync();
            if (roles.Any(x => x != "User"))
            {
                context.Result = new ObjectResult("Forbidden")
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                };
            }
            else
            {
                await next();
            }
        }
       
    }
}
