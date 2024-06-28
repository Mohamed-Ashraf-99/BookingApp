namespace Booking.Application.Emails.Helpers;

public class EmailSettings
{
    public int Port { get; set; }
    public string Host { get; set; }
    public string Password { get; set; }
    public string FromEmail { get; set; }
    public string DisplayName { get; set; }
}
