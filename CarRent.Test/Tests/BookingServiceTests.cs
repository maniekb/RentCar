using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using CarRent.Data.Models;
using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services;
using CarRent.Data.Services.Abstract;
using CarRent.Domain.Entities;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

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

        [Test]
        public void CreateBooking_Shuld_Throw_Exception_Wrong_Dates()
        {
            CarModel Car = new CarModel();
            Car.Id = 1;

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
                    Id= 1,
                    Email = "mail@mail.com",
                    Name = "Jan",
                    LastName = "Kowalski"
                },
                DateFrom= DateTime.Now.AddDays(10),
                DateTo= DateTime.Now.AddDays(15),
            };

            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(11);

            _bookingRepository.Setup(p => p.GetCarReservationsForDateRange(1, startDate, endDate)).Returns(new List<Booking>() { booking });

            // Act            
            var ex = Assert.Throws<Exception>(() => _bookingService.CreateBooking(1, Car, endDate, startDate));
            // Assert
            Assert.That(ex.Message, Is.EqualTo("Data koñca rezerwacji musi byæ wiêksza od daty pocz¹tku."));
        }

        [Test]
        public void CreateBooking_Shuld_Throw_Exception_Car_Not_Available()
        {
            CarModel Car = new CarModel();
            Car.Id = 1;

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
                    Id = 1,
                    Email = "mail@mail.com",
                    Name = "Jan",
                    LastName = "Kowalski"
                },
                DateFrom = DateTime.Now.AddDays(10),
                DateTo = DateTime.Now.AddDays(15),
            };


            var startDate = DateTime.Now;
            var startDatePlus10 = startDate.AddDays(10);
            var startDatePlus11 = startDate.AddDays(11);
            var startDatePlus12 = startDate.AddDays(12);
            var startDatePlus13 = startDate.AddDays(13);
            var startDatePlus23 = startDate.AddDays(23);

            _bookingRepository.Setup(p => p.GetCarReservationsForDateRange(1, startDate, startDatePlus11)).Returns(new List<Booking>() { booking });
            _bookingRepository.Setup(p => p.GetCarReservationsForDateRange(1, startDate, startDatePlus23)).Returns(new List<Booking>() { booking });
            _bookingRepository.Setup(p => p.GetCarReservationsForDateRange(1, startDatePlus12, startDatePlus13)).Returns(new List<Booking>() { booking });
            _bookingRepository.Setup(p => p.GetCarReservationsForDateRange(1, startDatePlus13, startDatePlus23)).Returns(new List<Booking>() { booking });

            // Act            
            var ex = Assert.Throws<Exception>(() => _bookingService.CreateBooking(1, Car, startDate, startDatePlus11));
            var ex2 = Assert.Throws<Exception>(() => _bookingService.CreateBooking(1, Car, startDate, startDatePlus23));
            var ex3 = Assert.Throws<Exception>(() => _bookingService.CreateBooking(1, Car, startDatePlus12, startDatePlus13));
            var ex4 = Assert.Throws<Exception>(() => _bookingService.CreateBooking(1, Car, startDatePlus13, startDatePlus23));


            var notAvailableMessage = "Ten pojazd nie jest dostêpny w wybranym terminie!";

            // Assert
            Assert.That(ex.Message, Is.EqualTo(notAvailableMessage));
            Assert.That(ex2.Message, Is.EqualTo(notAvailableMessage));
            Assert.That(ex3.Message, Is.EqualTo(notAvailableMessage));
            Assert.That(ex4.Message, Is.EqualTo(notAvailableMessage));
        }

        [Test]
        public void CreateBooking_Shuld_Add_New_Booking()
        {

            CarModel Car = new CarModel()
            {
                Id = 1,
                Number = "KRA12345",
                Brand = "Opel",
                Model = "Astra"
            };

            // Arrange
            var booking = new Booking()
            {
                Id = 1,
                Car = new Car()
                {
                    Id = 1,
                    Number = "KRA12345",
                    Brand = "Opel",
                    Model = "Astra"
                },
                User = new User()
                {
                    Id = 1,
                    Email = "mail@mail.com",
                    Name = "Jan",
                    LastName = "Kowalski"
                },
                DateFrom = DateTime.Now.AddDays(5),
                DateTo = DateTime.Now.AddDays(10),
            };

            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(3);

            // Act            
            _bookingRepository.Setup(p => p.GetCarReservationsForDateRange(1, startDate, endDate)).Returns(new List<Booking>() { });

            // Assert
            Assert.That(() => _bookingService.CreateBooking(1, Car, startDate, endDate), Throws.Nothing);
        }
    }
}