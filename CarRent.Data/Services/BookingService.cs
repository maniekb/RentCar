﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using CarRent.Common.Models;
using CarRent.Data.Migrations;
using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services.Abstract;
using CarRent.Domain.Entities;
using static System.Collections.Specialized.BitVector32;

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

        public BookingsModel GetBookingsForUser(int userId)
        {
            var pastBookings = _bookingRepository.GetPastBookingsForUser(userId);
            var currentBookings = _bookingRepository.GetCurrentBookingsForUser(userId);
            var upcomingBookings = _bookingRepository.GetUpcomingBookingsForUser(userId);

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
                Id = b.Id,
                User = $"{b.User.Name} {b.User.LastName} {b.User.Email}",
                Car = $"{b.Car.Brand} {b.Car.Model} {b.Car.Number}",
                DateFrom = b.DateFrom.ToString("dd-MM-yyyy"),
                DateTo = b.DateTo.ToString("dd-MM-yyyy")
            }).ToList();
        }

        public void RemoveBooking(int bookingId)
        {
            _bookingRepository.RemoveBooking(bookingId);
        }


        private bool isCarAvailable(int carId, DateTime dateFrom, DateTime dateTo)
        {

            if (dateFrom.CompareTo(dateTo) >= 0)
            {
                throw new Exception("Data końca rezerwacji musi być większa od daty początku.");
            }

            var foundBookings = _bookingRepository.GetCarReservationsForDateRange(carId, dateFrom, dateTo);

            if(foundBookings.Count > 0)
            {
                return false;
            }

            return true;
        }


        public async void CreateBooking(int userId, int carId, DateTime dateFrom, DateTime dateTo)
        {
            var test = isCarAvailable(carId, dateFrom, dateTo);


        }
    }
}
