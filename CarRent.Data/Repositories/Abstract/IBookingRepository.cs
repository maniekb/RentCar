using CarRent.Domain.Entities;
using System.Collections.Generic;

namespace CarRent.Data.Repositories.Abstract
{
    public interface IBookingRepository
    {
        List<Booking> GetCurrentBookings();
        List<Booking> GetPastBookings();
        List<Booking> GetUpcomingBookings();
    }
}
