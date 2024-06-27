using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.ApplicationUser.Queries.GetHotelById.Dto
{
    public class PackageDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public List<RoomFacilitiesDto> RoomFacilities { get; set; }
        public List<MealsDto> Meals { get; set; }
        public List<PackageFacilitiesDto> PackageFacilities { get; set; }
    }

}
