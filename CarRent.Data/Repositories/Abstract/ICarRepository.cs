﻿using CarRent.Domain.Entities;
using System.Collections.Generic;

namespace CarRent.Data.Repositories.Abstract
{
    public interface ICarRepository
    {
        public List<Car> GetAll();
    }
}