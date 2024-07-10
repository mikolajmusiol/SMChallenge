using BookingTask.Models.DTOs;

namespace BookingTask.Services.Interfaces
{
    public interface IDeskService
    {
        Task<IEnumerable<DeskDto>> GetDesks(string? location);
        Task<int> Add(AddDeskDto deskDto);
        Task Delete(int id);
        Task MakeUnavailable(int id);
    }
}
