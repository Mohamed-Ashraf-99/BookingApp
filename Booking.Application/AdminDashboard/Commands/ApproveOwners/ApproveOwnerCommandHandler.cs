
using Booking.Application.Services.Email;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Booking.Application.AdminDashboard.Commands.ApproveOwners;

public class ApproveOwnerCommandHandler(ILogger<ApproveOwnerCommandHandler> _logger,
    IOwnerRepository _ownerRepository,
    IEmailService _emailService, IUrlHelper _urlHelper,
    UserManager<User> _userManager, IHttpContextAccessor _httpContextAccessor) : IRequestHandler<ApproveOwnerCommand, string>
{
    public async Task<string> Handle(ApproveOwnerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Approve Owner with id : {request.Id}");

        try
        {
            var owner = await _ownerRepository.GetOwnerById(request.Id);
            if (owner == null)
            {
                _logger.LogWarning($"Owner with id {request.Id} not found.");
                return "Owner Not Found";
            }

            owner.IsVerified = true;
            var updateResult = await _ownerRepository.UpdateOwnerAsync(owner);

            if (!updateResult)
            {
                _logger.LogError($"Failed to update owner with id {request.Id}.");
                return "Failed to approve owner.";
            }

            await SendEmailConfirmation(request.Id);

            return "Owner approved successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error approving owner with id {request.Id}: {ex.Message}");
            return $"Error: {ex.Message}";
        }
    }

    private async Task<string> SendEmailConfirmation(int ownerId)
    {
        var userId = await _ownerRepository.GetUserIdByOwnerId(ownerId);
        var user = await _userManager.FindByIdAsync(userId.ToString());


        // Construct the confirmation URL
        var requestAccessor = _httpContextAccessor.HttpContext.Request;
        var returnUrl = $"http://localhost:4200/login";

        // Read HTML email template from file
        var filePath = $"{Directory.GetCurrentDirectory()}\\Template\\OwnerApprovedTemplate.html";
        var mailText = await File.ReadAllTextAsync(filePath);

        // Replace placeholders in the template with actual data
        mailText = mailText.Replace("[username]", user.UserName)
                           .Replace("[email]", user.Email)
                           .Replace("[returnUrl]", returnUrl);

        // Send the email
        await _emailService.SendEmailAsync(user.Email, mailText, "Certificate Approved");

        return "Email sent successfully.";
    }

}
