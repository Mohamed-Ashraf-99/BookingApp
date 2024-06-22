using AutoMapper;
using Booking.Application.Services.Authentication;
using Booking.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.Application.Authentication.Commands
{
    public class SignInCommandHandler(ILogger<SignInCommandHandler> _logger,
        UserManager<User> _userManager,
        SignInManager<User> _signInManager,
        IAuthenticationServices _authenticationService) : IRequestHandler<SignInCommand, string>
    {
        public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Sign in attempt for User with Email: {request.Email}");

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    _logger.LogWarning($"User with Email: {request.Email} not found.");
                    return "User not found";
                }

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!signInResult.Succeeded)
                {
                    _logger.LogWarning($"Sign in failed for User with Email: {request.Email}");
                    return "Failed";
                }

                var accessToken = await _authenticationService.GetJWTToken(user);
                return accessToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during sign-in for User with Email: {request.Email}");
                throw new Exception(ex.Message); 
            }
        }
    }
}
