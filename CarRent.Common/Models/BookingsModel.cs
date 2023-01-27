using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Common.Models
{
    public class BookingsModel
    {
        public List<BookingModel> UpcomingBookings { get; set; }
        public List<BookingModel> CurrentBookings { get; set; }
        public List<BookingModel> PastBookings { get; set; }
    }
}
