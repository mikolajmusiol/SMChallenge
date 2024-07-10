using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;
using DeskBooking.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingTask.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost]
        public async Task<ActionResult> AddLocation([FromBody] AddLocationDto locationDto)
        {
            int id = await _locationService.Add(locationDto);
            return Created($"api/locations/{id}", null);
        }
    }
}
