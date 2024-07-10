using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;

namespace BookingTask.Services
{
    public class BookingService : IBookingService
    {
        public async Task<IEnumerable<BookingDto>> GetBookings()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Book(AddBookingDto bookingDto)
        {
            throw new NotImplementedException();
        }

        public async Task Change(int id, ChangeDeskDto changeDeskDto)
        {
            throw new NotImplementedException();
        }

        
    }
}
