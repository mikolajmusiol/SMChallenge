namespace DeskBooking.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookedDay { get; set; }

        public Desk? Desk { get; set; }
        public User? User { get; set; }
    }
}
