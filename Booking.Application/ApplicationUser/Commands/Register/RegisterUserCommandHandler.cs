using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Booking.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;


namespace Booking.Application.ApplicationUser.Commands.Register;

public class RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> _logger,
    IMapper _mapper,
    UserManager<User> _userManager) : IRequestHandler<RegisterUserCommand, string>
{
    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Attempting to register user with username: {request.UserName}");

        // Check if email already exists
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            _logger.LogWarning($"Registration failed: Email {request.Email} already exists.");
            return "Email already exists";
        }

        var user = _mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError($"User registration failed for {request.UserName}. Errors: {errors}");
            return $"Registration failed: {errors}";
        }
        var users = await _userManager.Users.ToListAsync();
        if (users.Count > 0)
            await _userManager.AddToRoleAsync(user, "User");
        else
            await _userManager.AddToRoleAsync(user, "Admin");

        _logger.LogInformation($"User {request.UserName} successfully registered.");
        return "Registration succeeded";
    }
}
