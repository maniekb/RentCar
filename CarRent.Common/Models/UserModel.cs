namespace CarRent.App.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
