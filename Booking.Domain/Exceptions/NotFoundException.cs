using System.Xml.Linq;

namespace Booking.Domain.Exceptions;

public class NotFoundException : Exception
{
    public  NotFoundException(string resourceType, string resourceIdentifier) : base($"{resourceType} with id: {resourceIdentifier} doesn't exist")
    {

    }
    public NotFoundException(string resourceType) : base($"Entity \"{resourceType}\" was not found.")
    {

    }
}
