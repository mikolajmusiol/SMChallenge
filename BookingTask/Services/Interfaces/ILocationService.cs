using BookingTask.Models.DTOs;
using DeskBooking.Entities;

namespace BookingTask.Services.Interfaces
{
    public interface ILocationService
    {
        Task<int> Add(AddLocationDto locationDto);
        Task Delete(int id);
        Task<List<Location>> GetLocations();
    }
}
