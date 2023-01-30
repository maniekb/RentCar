namespace CarRent.Data.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public string CarClass { get; set; }
        public string FuelType { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
