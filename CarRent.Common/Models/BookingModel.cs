namespace CarRent.Common.Models
{
    public class BookingModel
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Car { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
