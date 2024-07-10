using BookingTask.Models.DTOs;

namespace BookingTask.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetBookings();
        Task<int> Book(AddBookingDto bookingDto);
        Task Change(int id, ChangeDeskDto changeDeskDto);
    }
}
