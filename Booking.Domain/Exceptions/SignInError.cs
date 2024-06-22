namespace Booking.Domain.Exceptions;

public class SignInError : Exception
{
    public SignInError(string error) : base(error) 
    {
        
    }
}
