﻿using MediatR;

namespace Booking.Application.ApplicationUser.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<string>
{
    public int Id { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
