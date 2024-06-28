using Booking.Application.ApplicationUser.Commands.AddHotelsToWishList;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Commands.OwnerCrud.AddHotels
{
    public class AddHotelsCommandHandler(ILogger<AddHotelsCommandHandler> _logger,
    IOwnerRepository ownerRepository, IWebHostEnvironment _hostingEnvironment ) : IRequestHandler<AddHotelsCommand, string>
    {

        private void InsertImage(List<string> imageBase64Data, int hotelId)
        {
            bool isFirstImage = true; // Flag to track the first image

            foreach (var base64Data in imageBase64Data)
            {
                if (!string.IsNullOrEmpty(base64Data))
                {
                    var imageBytes = Convert.FromBase64String(base64Data);
                    var fileName = Guid.NewGuid() + ".jpg"; // Generate a unique file name
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);

                    // Save the image file to the server
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        stream.Write(imageBytes, 0, imageBytes.Length);
                    }

                    // Determine IsMain based on isFirstImage flag
                    var isMain = isFirstImage ? 1 : 0;
                    isFirstImage = false; // Set isFirstImage to false after processing the first image

                    // Save the image path to the database
                    var image = new Images { source = "/images/" + fileName, IsMain = isMain, HotelID = hotelId };
                    ownerRepository.AddImagesForHotels(image);
                }
            }
        }
        public async Task<string> Handle(AddHotelsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ownerId = await ownerRepository.GetOwnerIdByUserId(request.userId);
                if (ownerId == null)
                {
                    _logger.LogWarning($"Owner not found for user ID {request.userId}");
                    return "Owner not found";
                }

                var hotel = new Hotel
                {
                    Name = request.name,
                    Description = request.description,
                    NumberOfStars = request.numOfStars,
                    Address = new Address
                    {
                        City = request.city,
                        Street = request.street,
                        PostalCode = request.postalCode
                    }
                };

                ownerRepository.AddHotels(hotel);
               

                InsertImage(request.imagesSource, hotel.Id);

                _logger.LogInformation($"Hotel {hotel.Name} added successfully for owner {ownerId}.");
                return $"Hotel {hotel.Name} added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the hotel");
                return "An error occurred while adding the hotel";
            }
        }
    }
}
