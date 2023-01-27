﻿using CarRent.Domain.Enums;

namespace CarRent.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public CarClassEnum CarClass { get; set; }
        public int FuelInTank { get; set; }
        public int TankCapacity { get; set; }
    }
}