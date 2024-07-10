namespace BookingTask.Models.DTOs
{
    public class BookingDto
    {
        public string UserName { get; set; }
        public DateTime BookedDay { get; set; }
        public int DeskId { get; set; }
        public string Country {  get; set; }
        public string City {  get; set; }
        public string Street {  get; set; }
    }
}
