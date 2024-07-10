namespace DeskBooking.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public List<Desk> Desks { get; set; }
    }
}
