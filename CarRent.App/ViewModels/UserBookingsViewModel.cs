using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using CarRent.Common.Models;
using CarRent.Data.Services.Abstract;

namespace CarRent.App.ViewModels
{
    public class UserBookingsViewModel : ViewModelBase
    {
        private readonly IBookingService _bookingsService;
        private int _userId;
        public ICommand RemoveBooking { get; }

        private BookingsModel _bookings;
        public BookingsModel Bookings
        {
            get
            {
                return _bookings;
            }
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }


        public UserBookingsViewModel(int userId, IBookingService bookingsService)
        {
            _bookingsService = bookingsService;
            _userId = userId;
            RemoveBooking = new ViewModelCommand(p => ExecuteRemovBookingCommand(p));

            LoadViewData();

        }

        private void LoadViewData()
        {
            Bookings = _bookingsService.GetBookingsForUser(_userId);
        }

        private async void ExecuteRemovBookingCommand(object obj)
        {

            _bookingsService.RemoveBooking((int)obj);
            Bookings = _bookingsService.GetBookings();
        }
    }
}
