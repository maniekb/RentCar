using CarRent.Data.Persistence;
using CarRent.Data.Repositories.Abstract;
using CarRent.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarRent.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CarRentDbContext _context;

        public BookingRepository(CarRentDbContext context)
        {
            _context = context;
        }

        public List<Booking> GetCurrentBookings()
            => _context.Bookings
                .Include(x => x.Car)
                .Include(x => x.User)
                .Where(x => x.DateFrom < DateTime.Now && x.DateTo > DateTime.Now).ToList();

        public List<Booking> GetPastBookings()
            => _context.Bookings
                .Include(x => x.Car)
                .Include(x => x.User)
                .Where(x => x.DateTo < DateTime.Now).ToList();

        public List<Booking> GetUpcomingBookings()
            => _context.Bookings
                .Include(x => x.Car)
                .Include(x => x.User)
                .Where(x => x.DateFrom > DateTime.Now).ToList();

        public List<Booking> GetCurrentBookingsForUser(int userID)
            => _context.Bookings
                .Include(x => x.Car)
                .Include(x => x.User)
                .Where(x => x.User.Id == userID && x.DateFrom < DateTime.Now && x.DateTo > DateTime.Now)
                .ToList();

        public List<Booking> GetPastBookingsForUser(int userID)
            => _context.Bookings
                .Include(x => x.Car)
                .Include(x => x.User)
                .Where(x => x.User.Id == userID && x.DateTo < DateTime.Now)
                .ToList();

        public List<Booking> GetUpcomingBookingsForUser(int userID)
            => _context.Bookings
                .Include(x => x.Car)
                .Include(x => x.User)
                .Where(x => x.User.Id == userID && x.DateFrom > DateTime.Now)
                .ToList();
    }
}
