
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.Data.Persistence;
using CarRent.Data.Repositories.Abstract;
using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            => _context.Cars.ToList();

        public Car GetByNumber(string number)
            => _context.Cars.FirstOrDefault(x => x.Number.ToLower() == number.ToLower());
        

        public void AddCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }
    }
}
