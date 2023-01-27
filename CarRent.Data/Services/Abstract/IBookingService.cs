using System;
using System.Collections.Generic;
using System.Text;
using CarRent.Common.Models;

namespace CarRent.Data.Services.Abstract
{
    public interface IBookingService
    {
        BookingsModel GetBookings();
    }
}
