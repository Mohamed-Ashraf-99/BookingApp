using Booking.Application.Authentication.Helpers;
using Booking.Application.Services.Authentication;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Authentication.Commands.SignIn;

public class SignInCommandHandler(ILogger<SignInCommandHandler> _logger,
    UserManager<User> _userManager,
    SignInManager<User> _signInManager,
    IAuthenticationServices _authenticationService) : IRequestHandler<SignInCommand, JwtAuthResult>
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

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            
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
