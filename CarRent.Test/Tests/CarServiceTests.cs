using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services;
using CarRent.Domain.Entities;
using CarRent.Domain.Enums;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CarRent.Test.Tests
{
    public class CarServiceTests
    {
        private Mock<ICarRepository> _carRepository;
        private CarService _carService;

        [SetUp]
        public void Setup()
        {
            _carRepository = new Mock<ICarRepository>();
            _carService = new CarService(_carRepository.Object);
        }

        [Test]
        public void GetAll_Should_Return_Proper_Values()
        {
            // Arrange
            var cars = new List<Car>()
            {
                new Car()
                {
                    Number = "KRA12345",
                    Brand = "Fiat",
                    Model = "Panda",
                    CarClass = CarClassEnum.BUDGET
                },
                new Car()
                {
                    Number = "KRA54321",
                    Brand = "Opel",
                    Model = "Astra",
                    CarClass = CarClassEnum.NORMAL
                }
            };
            _carRepository.Setup(p => p.GetAll()).Returns(cars);

            // Act
            var allCars = _carService.GetAll();

            // Assert
            Assert.NotNull(allCars);

            Assert.AreEqual(2, allCars.Count);
            Assert.That(allCars.Where(x => x.CarClass == "Normalne").Count() == 1);
            Assert.That(allCars.Where(x => x.CarClass == "Budzet").Count() == 1);
        }

        [Test]
        public void AddCar_Should_Return_False_If_Car_With_Number_Exists()
        {
            // Arrange
            var existingCar = new Car()
            {
                Number = "KRA12345",
                Brand = "Fiat",
                Model = "Panda",
                CarClass = CarClassEnum.BUDGET
            };

            _carRepository.Setup(p => p.GetByNumber(existingCar.Number)).Returns(existingCar);

            // Act
            var result = _carService.AddCar(existingCar.Number, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CarClassEnum>(),
                It.IsAny<FuelTypeEnum>(), It.IsAny<int>(), It.IsAny<decimal>());

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void AddCar_Should_Return_True_If_Car_With_Number_Not_Exists()
        {
            // Arrange
            _carRepository.Setup(p => p.GetByNumber(It.IsAny<string>())).Returns((Car)null);

            // Act
            var result = _carService.AddCar("KRA 12345", It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CarClassEnum>(),
                It.IsAny<FuelTypeEnum>(), It.IsAny<int>(), It.IsAny<decimal>());

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteCar_Should_Return_False_If_Car_With_Number_Not_Exists()
        {
            // Arrange
            _carRepository.Setup(p => p.GetByNumber("KRA 12345")).Returns((Car)null);

            // Act
            var result = _carService.DeleteCar("KRA 12345");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteCar_Should_Return_True_If_Car_With_Number_Exists()
        {
            var existingCar = new Car()
            {
                Number = "KRA12345",
                Brand = "Fiat",
                Model = "Panda",
                CarClass = CarClassEnum.BUDGET
            };

            // Arrange
            _carRepository.Setup(p => p.GetByNumber(existingCar.Number)).Returns(existingCar);
            _carRepository.Setup(p => p.DeleteCar(existingCar)).Returns(true);

            // Act
            var result = _carService.DeleteCar(existingCar.Number);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
