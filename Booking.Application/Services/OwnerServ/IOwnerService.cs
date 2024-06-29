using Booking.Application.RegisterAsOwner.Commands.OwnerRegister;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;

namespace Booking.Application.Services.OwnerServ;

public interface IOwnerService
{
    Task CreateOwnerAsync(Owner owner);
}
