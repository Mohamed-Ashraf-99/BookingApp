using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.DeleteRoom
{
    public class DeleteRoomCommand(int Id) : IRequest<string>
    {
        public int RoomId { get; set; } = Id;
    }
}
