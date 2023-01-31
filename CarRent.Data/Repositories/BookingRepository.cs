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

        public List<Booking> GetCarReservationsForDateRange(int carId, DateTime dateFrom, DateTime dateTo)
           => _context.Bookings
               .Include(x => x.Car)
               .Where(x => x.Car.Id == carId && (dateFrom <= x.DateFrom && dateTo >= x.DateTo || dateFrom <= x.DateFrom && dateTo >= x.DateFrom || dateFrom >= x.DateFrom && dateTo <= x.DateTo || dateFrom <= x.DateTo && dateTo >= x.DateTo))
               .ToList();

        public void AddBooking(int userId, int carId, DateTime dateFrom, DateTime dateTo, decimal totalPrice)
        {
            var booking = new Booking
            {
                UserId = userId,
                CarId = carId,
                DateFrom = dateFrom,
                DateTo = dateTo,
                TotalPrice = totalPrice
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public bool RemoveBooking(int bookingId)
        {

            var local = _context.Set<Booking>()
               .Local
               .FirstOrDefault(entry => entry.Id.Equals(bookingId));

            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
                _context.Bookings.Remove(local);
                _context.SaveChanges();
                return true;
            }
            return false;
            // set Modified flag in your entry
            /*
            _context.Entry(entryToUpdate).State = EntityState.Modified;

            Booking booking = new Booking() { Id = bookingId };
            _context.Bookings.Attach(booking);
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return true;*/
        }
    }
}
