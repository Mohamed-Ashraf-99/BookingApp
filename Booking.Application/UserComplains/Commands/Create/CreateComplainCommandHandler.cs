

using AutoMapper;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Booking.Application.UserComplains.Commands.Create;

public class CreateComplainCommandHandler(ILogger<CreateComplainCommandHandler> _logger,
    IComplainsRepository _complainsRepository,
    IMapper _mapper) : IRequestHandler<CreateComplainCommand, string>
{
    public async Task<string> Handle(CreateComplainCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Client with id {request.ClientId} Make a completion");
        var complain = _mapper.Map<Complains>(request);
        var response = await _complainsRepository.CreateAsync(complain);
        if (response > 0)
            return "Succeeded";
        return "Failed";
    }
}
