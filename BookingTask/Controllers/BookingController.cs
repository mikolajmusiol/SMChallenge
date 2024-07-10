using BookingTask.Migrations;
using BookingTask.Models.DTOs;
using BookingTask.Services;
using BookingTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingTask.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            var bookingsDtos = await _bookingService.GetBookings();
            return Ok(bookingsDtos);
        }

        [HttpPost]
        public async Task<ActionResult> BookDesk([FromBody] AddBookingDto bookingDto)
        {
            int id = await _bookingService.Book(bookingDto);
            return Created($"api/bookings/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ChangeDesk([FromRoute] int id, [FromBody] ChangeDeskDto changeDeskDto)
        {
            await _bookingService.Change(id, changeDeskDto);
            return NoContent();
        }
    }
}
