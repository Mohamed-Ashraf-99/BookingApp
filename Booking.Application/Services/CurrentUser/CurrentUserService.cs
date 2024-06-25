using Booking.Application.Authentication.Helpers;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Booking.Application.Services.CurrentUser;

public class CurrentUserService(IHttpContextAccessor _httpContextAccessor,
    UserManager<User> _userManager) : ICurrentUserService
{

    public async Task<int> GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(claim => claim.Type == nameof(UserClaimModel.Id));

        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            throw new NotFoundException(nameof(User), "User ID claim not found or invalid.");

        if (!int.TryParse(userIdClaim.Value, out int userId))
              throw new Exception("Can't Parse");

        return userId;
    }
    
    public async Task<User> GetUserAsync()
    {
        var userId =await GetUserId();
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if(user is null)
            throw new UnauthorizedAccessException($"User with ID : {userId} unauthorized");
        return user;
    }
   
    public async Task<List<string>> GetCurrentUserRolesAsync()
    {
        var user = await GetUserAsync();
        var roles = await  _userManager.GetRolesAsync(user);
        return roles.ToList();
    }
}
