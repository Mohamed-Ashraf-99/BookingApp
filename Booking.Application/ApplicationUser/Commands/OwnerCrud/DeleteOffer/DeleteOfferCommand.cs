using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.DeleteOffer
{
    public class DeleteOfferCommand(int Id) : IRequest<string>
    {
        public int OfferId { get; set; } = Id;
    }
}
