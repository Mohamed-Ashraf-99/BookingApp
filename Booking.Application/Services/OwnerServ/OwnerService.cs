using AutoMapper;
using Booking.Application.RegisterAsOwner.Commands.OwnerRegister;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Booking.Application.Services.OwnerServ;

public class OwnerService(IMapper _mapper, IOwnerRepository _ownerRepository,
    ILogger<OwnerService> _logger) : IOwnerService
{
    public async Task CreateOwnerAsync(Owner owner)
    {
        _logger.LogInformation($"Create Owner with UserName: {owner.User?.UserName}");
        await _ownerRepository.CreateAsync(owner);
    }
}
