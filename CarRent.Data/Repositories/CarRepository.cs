
using CarRent.Data.Persistence;
using CarRent.Data.Repositories.Abstract;
using CarRent.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CarRent.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentDbContext _context;

        public CarRepository(CarRentDbContext context)
        {
            _context = context;
        }


        public List<Car> GetAll()
            => _context.Cars.Where(x => !x.IsDeleted).ToList();

        public bool DeleteCar(Car car)
        {
            if (car != null)
            {
                car.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public Car GetByNumber(string number)
            => _context.Cars.FirstOrDefault(x => x.Number.ToLower() == number.ToLower());
        

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }
    }
}
