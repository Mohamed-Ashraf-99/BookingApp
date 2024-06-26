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
        public async Task AddOffer(Offer offer)
        {
            await _context.Offers.AddAsync(offer);
            _context.SaveChanges();
        }

        public async Task DeleteExpiredOffer()
        {
            var expiredOffers = await _context.Offers
               .Where(r => r.EndDate <= DateTime.Now)
               .ToListAsync();

            foreach (var offer in expiredOffers)
            {
                offer.IsDeleted = true;
                _context.Offers.Update(offer);
            }
            await _context.SaveChangesAsync();

        }

        public async Task DeleteOffer(int offerId)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            offer.IsDeleted = true;
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
            
        }
        public async Task<IEnumerable<Offer>> GetAllOffersAsync()
        {
            return await _context.Offers.Include(off => off.Hotel).ThenInclude(off=>off.Images).Where(off => off.IsDeleted != true).ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetOffersByHotelIdAsync(int Id)
        {
            return await _context.Offers.Include(o => o.Hotel).ThenInclude(off => off.Images).Where(o => o.HotelId == Id && o.IsDeleted != true).ToListAsync();

        }
    }
}
