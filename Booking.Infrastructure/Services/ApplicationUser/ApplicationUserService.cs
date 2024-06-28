


using AutoMapper;
using Booking.Application.Services.Email;
using Booking.Domain.Entities.Identity;
using Booking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Services.ApplicationUser;

public class ApplicationUserService(ILogger<ApplicationUserService> _logger,
    UserManager<User> _userManager,
    IHttpContextAccessor _httpContextAccessor,
    IEmailService _emailService,
    BookingDbContext _context,
    IUrlHelper _urlHelper) : IApplicationUserService
{
    public async Task<string> CreateUserAsync(User user, string password)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                _logger.LogWarning($"Registration failed: Email {user.Email} already exists.");
                return "Email already exists";
            }

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError($"User registration failed for {user.UserName}. Errors: {errors}");
                return $"Registration failed: {errors}";
            }
            var users = await _userManager.Users.ToListAsync();

            if (users.Count > 0)
                await _userManager.AddToRoleAsync(user, "User");
            else
                await _userManager.AddToRoleAsync(user, "Admin");

            // Send Confirmation mail
            await SendEmailConfirmation(user);

            await transaction.CommitAsync();
            _logger.LogInformation($"User {user.UserName} successfully registered.");
            return "Registration succeeded";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return "Failed";
        }
    }

    private async Task<string> SendEmailConfirmation(User user)
    {
        // Send Confirmation mail

        //1->> Generate email code confirmation 
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //2->>create the code to email to click on it to verify 
        var requestAccessor = _httpContextAccessor.HttpContext.Request;


        var returnUrl = $"{requestAccessor.Scheme}://{requestAccessor.Host}{_urlHelper.Action("ConfirmEmail", "Authentication", new { UserId = user.Id, Code = code })}";
        //var returnUrl = $"{requestAccessor.Scheme}://{requestAccessor.Host}/api/Authentication/ConfirmEmail?userId={user.Id}&code={code}";

        var filePath = $"{Directory.GetCurrentDirectory()}\\Template\\ConfirmEmailTemplate.html";

        var str = new StreamReader(filePath);

        var mailText = str.ReadToEnd();
        str.Close();

        mailText = mailText.Replace("[username]", user.UserName).Replace("[email]", user.Email).Replace("[returnUrl]", returnUrl);

        //3->> Send the code to user email
        await _emailService.SendEmailAsync(user.Email, mailText, "Welcome to our website");

        return "Email sent successfully.";
    }
}
