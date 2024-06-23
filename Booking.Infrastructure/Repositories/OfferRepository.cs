﻿using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class OfferRepository(BookingDbContext _context) : IOfferRepository
    {
        public async Task<IEnumerable<Offer>> GetAllOffersAsync()
        {
            return await _context.Offers.Include(off => off.Hotel).Where(off => off.IsDeleted != true).ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetOffersByHotelIdAsync(int Id)
        {
            return await _context.Offers.Include(o => o.Hotel).Where(o => o.HotelId == Id && o.IsDeleted != true).ToListAsync();

        }
    }
}