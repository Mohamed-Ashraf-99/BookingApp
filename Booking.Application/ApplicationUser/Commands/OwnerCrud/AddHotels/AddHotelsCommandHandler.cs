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

       
        private void InsertImage(List<IFormFile> ImageUrl, int HotelId)
        {
            bool isFirstImage = true; // Flag to track the first image

            foreach (var file in ImageUrl)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    // Construct the image path with the file name and extension
                    var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", fileName);

                    // Save the file to the server
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Determine IsMain based on isFirstImage flag
                    var isMain = isFirstImage ? 1 : 0;
                    isFirstImage = false; // Set isFirstImage to false after processing the first image

                    // Save the image path to the database
                    var image = new Images { source = "https://localhost:7182" + "/images/" + fileName, IsMain = isMain, HotelID = HotelId };
                    ownerRepository.AddImagesForHotels(image);
                }
            }
            ownerRepository.UpdateChanges();
        }

        public async Task<string> Handle(AddHotelsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.userId = await ownerRepository.GetOwnerIdByUserId(request.userId);
                if (request.userId == null)
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
                    },
                    IsDeleted = false,
                    OwnerId= request.userId                   
                };

                int hotelid= await ownerRepository.AddHotels(hotel);


                InsertImage(request.imagesSource, hotelid);

                _logger.LogInformation($"Hotel {hotel.Name} added successfully for owner {request.userId}.");
                return $"Hotel {hotel.Name} added successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the hotel");
                return ex.Message;
            }
        }
    }
}
