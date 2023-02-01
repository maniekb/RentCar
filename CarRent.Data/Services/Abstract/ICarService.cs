using CarRent.Data.Models;
using System.Collections.Generic;
using CarRent.Domain.Enums;

namespace CarRent.Data.Services.Abstract
{
    public interface ICarService
    {
        public List<CarModel> GetAll();
        public bool AddCar(string number, string brand, string model, CarClassEnum carClass, FuelTypeEnum fuelType, int yearOfProduction, decimal pricePerDay);
    }
}
