using CarRent.Data.Repositories.Abstract;
using CarRent.Data.Services.Abstract;
using CarRent.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Common.Authentication.Helpers;
using CarRent.Data.Models;

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
                Brand = c.Brand,
                Model = c.Model,
                YearOfProduction = c.YearOfProduction,
                CarClass = CarClassHelper.ResolveCarClass(c.CarClass),
                Tank = $"{c.FuelInTank}/{c.TankCapacity}L"
            }).ToList();
        }
    }
}
