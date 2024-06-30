


using AutoMapper;
using Booking.Application.RegisterAsOwner.Commands.OwnerRegister;
using Booking.Application.Services.Email;
using Booking.Application.Services.FileUpload;
using Booking.Application.Services.OwnerServ;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
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
    IUrlHelper _urlHelper,
    IMapper _mapper,
    IFileService _fileService
    ,IOwnerService _ownerService, IClientRepository _clientRepository) : IApplicationUserService
{
    public async Task<string> CreateOwnerAsync(User user, string password, IFormFile formFile)
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

            // Upload certificate and get the URL
            var certificateUrl = await _fileService.UploadImage("OwnerCertificate", formFile);
            if (certificateUrl.StartsWith("Failed"))
            {
                return "Failed to upload certificate";
            }

            // Format the certificate URL
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{context.Scheme}://{context.Host}/";
            var formattedCertificateUrl = baseUrl + certificateUrl.TrimStart('/');

            user.Certificate = formattedCertificateUrl;

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogError($"User registration failed for {user.UserName}. Errors: {errors}");
                return $"Registration failed: {errors}";
            }

            await _userManager.AddToRoleAsync(user, "Owner");

            // Create the User
            var owner = _mapper.Map<Owner>(user);
            owner.Certificate = formattedCertificateUrl;
            await _ownerService.CreateOwnerAsync(owner);

            // Send Confirmation mail
            await SendOwnerEmailConfirmation(user);

            await transaction.CommitAsync();
            _logger.LogInformation($"User {user.UserName} successfully registered.");
            return "Registration succeeded";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError($"Transaction rolled back. Error: {ex.Message}");
            return "Failed";
        }
    }



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

            // Determine user role based on existing users
            var users = await _userManager.Users.ToListAsync();
            if (users.Count > 0)
            {
                await _userManager.AddToRoleAsync(user, "User");

               
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            // Create the User
          

            var client = _mapper.Map<Client>(user);
            await _clientRepository.CreateAsync(client);


            // Send Confirmation mail
            await SendEmailConfirmation(user);

            await transaction.CommitAsync();

            
            _logger.LogInformation($"User {user.UserName} successfully registered.");
            return "Registration succeeded";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError($"User registration failed. Error: {ex.Message}");
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

    private async Task<string> SendOwnerEmailConfirmation(User user)
    {
        // Send Confirmation mail

        //1->> Generate email code confirmation 
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //2->>create the code to email to click on it to verify 
        var requestAccessor = _httpContextAccessor.HttpContext.Request;


        var returnUrl = $"{requestAccessor.Scheme}://{requestAccessor.Host}{_urlHelper.Action("ConfirmOwnerEmail", "Authentication", new { UserId = user.Id, Code = code })}";
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
