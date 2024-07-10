using BookingTask.Models.DTOs;
using DeskBooking.Entities;

namespace BookingTask.Services.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetBookings();
        Task<List<Booking>> Book(AddBookingDto bookingDto);
        Task Change(int bookingId, int deskId);
    }
}
