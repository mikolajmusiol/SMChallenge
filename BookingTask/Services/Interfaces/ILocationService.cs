using BookingTask.Models.DTOs;

namespace BookingTask.Services.Interfaces
{
    public interface ILocationService
    {
        Task<int> Add(AddLocationDto locationDto);
    }
}
