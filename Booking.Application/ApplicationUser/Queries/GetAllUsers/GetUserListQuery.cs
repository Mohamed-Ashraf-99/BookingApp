using Booking.Application.ApplicationUser.Queries.GetAllUsers.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllUsers
{
    public class GetUserListQuery : IRequest<IEnumerable<GetUserListDto>>
    {
    
    }
}
