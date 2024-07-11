using Microsoft.AspNetCore.Identity;

namespace DeskBooking.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
