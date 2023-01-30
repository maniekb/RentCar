using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Common.Models;
using CarRent.Data.Models;

namespace CarRent.Data.Services.Abstract
{
    public interface IBookingService
    {
        BookingsModel GetBookings();
        BookingsModel GetBookingsForUser(int userId);
        void CreateBooking(int userId, CarModel selectedCar, DateTime dateFrom, DateTime dateTo);

        void RemoveBooking(int bookingId);
        public decimal GetCalculatedPrice(decimal pricePerDay, DateTime dateFrom, DateTime dateTo);

    }
}
