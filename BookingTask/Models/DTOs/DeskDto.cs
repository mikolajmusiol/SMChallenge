using DeskBooking.Entities;

namespace BookingTask.Models.DTOs
{
    public class DeskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public LocationDto Location { get; set; }
    }
}
