using Booking.Application.ApplicationUser.Commands.DeleteUser;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Identity;
using Booking.Domain.Exceptions;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.DeleteRoom
{
    public class DeleteRoomCommandHandler(ILogger<DeleteRoomCommandHandler> _logger,
    IRoomRepository roomRepository) : IRequestHandler<DeleteRoomCommand, string>
    {

        public async Task<string> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting room with ID: {RoomId}", request.RoomId);

                var room = await roomRepository.GetByIdAsync(request.RoomId);
                if (room == null)
                {
                    _logger.LogWarning("Room with ID: {RoomId} not found", request.RoomId);
                    throw new NotFoundException(nameof(Room), request.RoomId.ToString());
                }

                await roomRepository.DeleteRoom(request.RoomId);

                _logger.LogInformation("Room with ID: {RoomId} deleted successfully", request.RoomId);
                return $"Room with ID: {request.RoomId} deleted successfully";
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Room with ID: {RoomId} not found", request.RoomId);
                return ex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting room with ID: {RoomId}", request.RoomId);
                return $"An unexpected error occurred while deleting room with ID: {request.RoomId}";
            }
        }
    }
}
