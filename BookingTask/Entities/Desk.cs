namespace DeskBooking.Entities
{
    public class Desk
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Location Location { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}