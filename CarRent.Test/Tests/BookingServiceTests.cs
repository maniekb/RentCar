using System.Collections.Generic;
using System.Linq;
using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services;
using CarRent.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace CarRent.Test.Tests
{
    public class BookingServiceTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private BookingService _bookingService;

        [SetUp]
        public void Setup()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _bookingService = new BookingService(_bookingRepository.Object);

        }

        [Test]
        public void GetBookings_Test_Should_Return_Proper_Values()
        {
            // Arrange
            var booking = new Booking()
            {
                Id = 1,
                Car = new Car()
                {
                    Number = "KRA12345",
                    Brand = "Opel",
                    Model = "Astra"
                },
                User = new User()
                {
                    Email = "mail@mail.com",
                    Name = "Jan",
                    LastName = "Kowalski"
                }
            };
            _bookingRepository.Setup(p => p.GetPastBookings()).Returns(new List<Booking>() { booking });
            _bookingRepository.Setup(p => p.GetCurrentBookings()).Returns(new List<Booking>());
            _bookingRepository.Setup(p => p.GetUpcomingBookings()).Returns(new List<Booking>());

            // Act
            var bookings = _bookingService.GetBookings();

            // Assert
            Assert.NotNull(bookings?.CurrentBookings);
            Assert.NotNull(bookings?.PastBookings);
            Assert.NotNull(bookings?.UpcomingBookings);

            Assert.AreEqual(1, bookings.PastBookings.Count);
            Assert.AreEqual(0, bookings.UpcomingBookings.Count);
            Assert.AreEqual(0, bookings.CurrentBookings.Count);

            var pastBooking = bookings.PastBookings.First();

            Assert.AreEqual($"{booking.Car.Brand} {booking.Car.Model} {booking.Car.Number}", pastBooking.Car);
        }
    }
}