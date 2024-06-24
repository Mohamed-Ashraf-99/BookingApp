using Booking.Application.ApplicationUser.Queries.GetAllReviews.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetAllReviews
{
    public class GetAllReviewsListQuery : IRequest<IEnumerable<RevieewDto>>
    {


    }
}
