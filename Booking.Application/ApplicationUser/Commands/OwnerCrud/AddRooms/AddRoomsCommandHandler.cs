using Booking.Application.ApplicationUser.Commands.OwnerCrud.AddHotels;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Enums;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.AddRooms
{
    public class AddRoomsCommandHandler(ILogger<AddRoomsCommandHandler> _logger,
    IRoomRepository roomRepository) : IRequestHandler<AddRoomsCommand, string>
    {
        public async Task<string> Handle(AddRoomsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                
                Enum.TryParse<RoomType>(request.roomType, true, out RoomType roomType);

                var room = new Room
                {
                    RoomType = roomType,
                    Description = request.description,
                    Price = request.price,
                    NumberOfBeds = request.numberOfBeds,
                    HotelId = request.hotelId
                };

                await roomRepository.AddRoomAsync(room);


               

                _logger.LogInformation($"Room added successfully for hotel {request.hotelId}.");
                return $"Room added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the room");
                return ex.Message;
            }
        }
    }
}
