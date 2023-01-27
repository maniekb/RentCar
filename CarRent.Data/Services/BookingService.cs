using System.Collections.Generic;
using System.Linq;
using CarRent.Common.Models;
using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services.Abstract;
using CarRent.Domain.Entities;

namespace CarRent.Data.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public BookingsModel GetBookings()
        {
            var pastBookings = _bookingRepository.GetPastBookings();
            var currentBookings = _bookingRepository.GetCurrentBookings();
            var upcomingBookings = _bookingRepository.GetUpcomingBookings();

            return new BookingsModel()
            {
                PastBookings = MapBookings(pastBookings),
                CurrentBookings = MapBookings(currentBookings),
                UpcomingBookings = MapBookings(upcomingBookings)
            };
        }

        private List<BookingModel> MapBookings(List<Booking> bookings)
        {
            return bookings.Select(b => new BookingModel()
            {
                User = $"{b.User.Name} {b.User.LastName} {b.User.Email}",
                Car = $"{b.Car.Brand} {b.Car.Model} {b.Car.Number}",
                DateFrom = b.DateFrom.ToString("dd-MM-yyyy"),
                DateTo = b.DateTo.ToString("dd-MM-yyyy")
            }).ToList();
        }
    }
}
