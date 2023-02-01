using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services.Abstract;
using CarRent.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CarRent.Common.Authentication.Helpers;
using CarRent.Data.Models;
using CarRent.Domain.Enums;

namespace CarRent.Data.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<CarModel> GetAll()
        {
            var cars = _carRepository.GetAll();

            return cars.Select(c => new CarModel()
            {
                Id = c.Id,
                Number = c.Number,
                Brand = c.Brand,
                Model = c.Model,
                YearOfProduction = c.YearOfProduction,
                CarClass = CarHelper.ResolveCarClass(c.CarClass),
                FuelType = CarHelper.ResolveFuelType(c.FuelType),
                PricePerDay = c.PricePerDay
            }).ToList();
        }

        public bool AddCar(string number, string brand, string model, CarClassEnum carClass, FuelTypeEnum fuelType,
            int yearOfProduction, decimal pricePerDay)
        {
            var car = _carRepository.GetByNumber(number);

            if (car != null)
                return false;

            car = new Car()
            {
                Number = Regex.Replace(number, @"\s+", "").ToUpper(),
                Brand = brand,
                Model = model,
                CarClass = carClass,
                FuelType = fuelType,
                YearOfProduction = yearOfProduction,
                PricePerDay = pricePerDay
            };

            _carRepository.AddCar(car);
            return true;
        }
    }
}
