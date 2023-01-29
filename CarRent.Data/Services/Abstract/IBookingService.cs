using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Common.Models;

namespace CarRent.Data.Services.Abstract
{
    public interface IBookingService
    {
        BookingsModel GetBookings();
        BookingsModel GetBookingsForUser(int userId);
        void CreateBooking(int userId, int carId, DateTime dateFrom, DateTime dateTo);

        void RemoveBooking(int bookingId);
    }
}
