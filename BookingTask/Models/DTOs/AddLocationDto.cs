using System.ComponentModel.DataAnnotations;

namespace BookingTask.Models.DTOs
{
    public class AddLocationDto
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
    }
}
