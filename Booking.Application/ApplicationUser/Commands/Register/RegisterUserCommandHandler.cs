using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Booking.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Booking.Application.Services.Email;
using Booking.Application.Services.ApplicationUser;


namespace Booking.Application.ApplicationUser.Commands.Register;

public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> _logger,
    IMapper _mapper,
    IApplicationUserService _applicationUserService) : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Attempting to register user with username: {request.UserName}");

        var user = _mapper.Map<User>(request);

        var result = await _applicationUserService.CreateUserAsync(user, user.PasswordHash);

        if (result != "Registration succeeded")
        {
            _logger.LogError($"User registration failed for {request.UserName}");
            return $"Registration failed";
        }

        return "Registration Succeeded";
    }
}
