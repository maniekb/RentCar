using CarRent.Domain.Entities;
using System.Collections.Generic;

namespace CarRent.Data.Repositories.Abstract
{
    public interface ICarRepository
    {
        public List<Car> GetAll();
        Car GetByNumber(string number);
        void AddCar(Car car);
        bool DeleteCar(Car car);
    }
}
