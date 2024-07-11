namespace BookingTask.Models.DTOs
{
    public class AddBookingDto
    {
        public int DeskId { get; set; }
        public List<DateTime> DaysOfReservation { get; set; }
    }
}
