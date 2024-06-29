

using AutoMapper;
using Booking.Application.RegisterAsOwner.Commands.OwnerRegister;
using Booking.Application.Services.ApplicationUser;
using Booking.Domain.Entities.Identity;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.ApplicationUser.Commands.OwnerRegister;

public class OwnerRegisterCommandHandler(ILogger<OwnerRegisterCommandHandler> _logger,
    IApplicationUserService _applicationUserService,
    IMapper _mapper) : IRequestHandler<OwnerRegisterCommand, string>
{
    public async Task<string> Handle(OwnerRegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var response = await _applicationUserService.CreateOwnerAsync(user,request.Password,request.Certificate);
        return response;
    }
}
