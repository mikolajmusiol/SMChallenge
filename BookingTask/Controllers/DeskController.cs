using BookingTask.Models.DTOs;
using BookingTask.Services;
using BookingTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingTask.Controllers
{
    [Route("api/desks")]
    [ApiController]
    public class DeskController : ControllerBase
    {
        private readonly IDeskService _deskService;

        public DeskController(IDeskService deskService)
        {
            _deskService = deskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeskDto>>> GetDesks([FromQuery]string? location)
        {
            var desksDtos = await _deskService.GetDesks(location);
            return Ok(desksDtos);
        }

        [HttpPost]
        public async Task<ActionResult> AddDesk([FromBody] AddDeskDto deskDto)
        {
            int id = await _deskService.Add(deskDto);
            return Created($"api/desks/{id}", null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDesk([FromRoute] int id)
        {
            await _deskService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> MakeUnavailable([FromRoute] int id)
        {
            await _deskService.MakeUnavailable(id);
            return NoContent();
        }
    }
}
