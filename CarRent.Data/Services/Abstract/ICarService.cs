using CarRent.Data.Models;
using System.Collections.Generic;

namespace CarRent.Data.Services.Abstract
{
    public interface ICarService
    {
        public List<CarModel> GetAll();
    }
}
