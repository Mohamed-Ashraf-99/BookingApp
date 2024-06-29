
using Booking.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.Services.ApplicationUser;

public interface IApplicationUserService
{
    Task<string> CreateUserAsync(User user, string password);
    Task<string> CreateOwnerAsync(User user, string password, IFormFile formFile);
}
