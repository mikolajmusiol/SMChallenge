using BookingTask.Migrations;
using BookingTask.Models.DTOs;
using BookingTask.Services;
using BookingTask.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingTask.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
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
            var bookings = await _bookingService.Book(bookingDto);
            return new ObjectResult(bookings) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ChangeDesk([FromRoute] int id, [FromQuery] int deskId)
        {
            await _bookingService.Change(id, deskId);
            return NoContent();
        }
    }
}
