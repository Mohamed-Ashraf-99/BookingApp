using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Booking.Commands.Checkout
{
    public class CreateCheckoutSessionCommand : IRequest<CreateCheckoutSessionResponse>
    {
        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
        public decimal Amount { get; set; }
    }

    public class CreateCheckoutSessionResponse
    {
        public string SessionId { get; set; }
        public string PublicKey { get; set; }
    }


}
