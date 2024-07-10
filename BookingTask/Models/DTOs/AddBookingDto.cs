namespace BookingTask.Models.DTOs
{
    public class AddBookingDto
    {
        public int DeskId { get; set; }
        public List<DateTime> DaysOfReservation { get; set; }
        public string Country {  get; set; }
        public string City {  get; set; }
        public string Street {  get; set; }
    }
}
