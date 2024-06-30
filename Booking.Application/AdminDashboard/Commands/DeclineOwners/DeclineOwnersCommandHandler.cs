
using Booking.Application.AdminDashboard.Commands.ApproveOwners;
using Booking.Application.Services.Email;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Booking.Application.AdminDashboard.Commands.DeclineOwners;

public class DeclineOwnersCommandHandler(ILogger<ApproveOwnerCommandHandler> _logger,
    IOwnerRepository _ownerRepository,
    IEmailService _emailService, IUrlHelper _urlHelper,
    UserManager<User> _userManager, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<DeclineOwnersCommand, string>
{
    public async Task<string> Handle(DeclineOwnersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Decline Owner with id : {request.Id}");
            var userId = await _ownerRepository.GetUserIdByOwnerId(request.Id);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await SendEmailConfirmation(request.Id);

            var result = await _ownerRepository.DeleteAsync(request.Id);
            if (!result)
            {
                _logger.LogError($"Failed to delete owner with id: {request.Id}");
                return "Failed to delete owner.";
            }

           
            
            if (user is null)
            {
                _logger.LogError($"User with id: {userId} not found.");
                return "User not found.";
            }

            await _userManager.DeleteAsync(user);

            

            return "Owner declined successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while declining owner with id: {request.Id}. Error: {ex.Message}");
            throw new Exception(ex.Message, ex);
        }
    }

    private async Task<string> SendEmailConfirmation(int ownerId)
    {
        var userId = await _ownerRepository.GetUserIdByOwnerId(ownerId);
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var requestAccessor = _httpContextAccessor.HttpContext.Request;

        var filePath = $"{Directory.GetCurrentDirectory()}\\Template\\OwnerRejectedTemplate.html";
        var mailText = await File.ReadAllTextAsync(filePath);

        // Replace placeholders in the template with actual data
        mailText = mailText.Replace("[username]", user.UserName);

        // Send the email
        await _emailService.SendEmailAsync(user.Email, mailText, "Certificate Rejected");

        return "Email sent successfully.";
    }

}
