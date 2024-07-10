using DeskBooking.Entities;

namespace BookingTask.Models.DTOs
{
    public class DeskDto
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string IsAvailable { get; set; }
        public DateTime? BookedDay { get; set; }
    }
}
