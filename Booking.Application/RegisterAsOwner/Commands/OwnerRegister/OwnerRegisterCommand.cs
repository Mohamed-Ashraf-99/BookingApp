using MediatR;
using Microsoft.AspNetCore.Http;

namespace Booking.Application.RegisterAsOwner.Commands.OwnerRegister;

public class OwnerRegisterCommand : IRequest<string>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile? Certificate { get; set; }

}
