﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities.Identity;

public class User : IdentityUser<int>
{
    public Address Address { get; set; }
}
