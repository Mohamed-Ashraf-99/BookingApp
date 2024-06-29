using Booking.Application.Authentication.Helpers;
using Booking.Application.Services.Authentication;
using Booking.Application.Services.CurrentUser;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Commands.SignIn;

public class SignInCommandHandler(ILogger<SignInCommandHandler> _logger,
    UserManager<User> _userManager,
    SignInManager<User> _signInManager,
    IAuthenticationServices _authenticationService,
    ICurrentUserService _currentUserService,
    IOwnerRepository _ownerRepository
    ) : IRequestHandler<SignInCommand, JwtAuthResult>
{

    public async Task<JwtAuthResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Sign in attempt for User with Email: {request.Email}");

        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning($"User with Email: {request.Email} not found.");
                throw new NotFoundException(nameof(User), user.Id.ToString());
            }

            // Check if User is Owner
            var isOwner = await _userManager.IsInRoleAsync(user, "Owner");

            if (isOwner)
            {
                var owner = await _ownerRepository.GetOwnerByUserId(user.Id);
                if (owner == null)
                {
                    _logger.LogWarning($"Owner record not found for User with Email: {request.Email}");
                    throw new NotFoundException(nameof(Owner), user.Id.ToString());
                }

                if (owner.IsVerified == false)
                {
                    _logger.LogWarning($"Owner with Email: {request.Email} is not verified.");
                    throw new SignInError("You are not verified yet!");
                }
            }


            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
            // Check if the email confirmed
            if(!user.EmailConfirmed)
            {
                throw new SignInError("Please confirm your email!");
            }

            if (!signInResult.Succeeded)
            {
                _logger.LogWarning($"Sign in failed for User with Email: {request.Email}");
                throw new SignInError("Password or Email Incorrect");
            }

            var result = await _authenticationService.GetJWTToken(user);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error during sign-in for User with Email: {request.Email}");
            throw new Exception(ex.Message);
        }
    }

}
