using Booking.Application.ApplicationUser.Queries.GetUserById.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetUserById;

public class GetUserByIdQuery(int id) : IRequest<GetUserByIdDto>
{
    public int UserId { get; set; } = id;
}
