using AutoMapper;
using BookingTask.Exceptions;
using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;
using BookingTask.Utilities;
using DeskBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingTask.Services
{
    public class BookingService : IBookingService
    {
        private readonly SMCDbContext _dbContext;
        private readonly IMapper _mapper;

        public BookingService(SMCDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> GetBookings()
        {
            var bookings = await _dbContext.Bookings
                .Include(x => x.Desk)
                .Include(x => x.User)
                .ToListAsync();
            //displayed data based on a role - to be finished
            var bookingsDtos = _mapper.Map<List<BookingDto>>(bookings);
            return bookingsDtos;
        }

        public async Task<List<Booking>> Book(AddBookingDto bookingDto)
        {
            if (!DateTimeChecking.AreDatesWithinAWeek(bookingDto.DaysOfReservation))
                throw new BadRequestException("Reservation can't exceed a week");

            var bookings = new List<Booking>();
            var desk = await GetDeskById(bookingDto.DeskId);
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == 1); //user context id

            if (desk is null || user is null)
            {
                throw new NotFoundException("Couldn't find");
            }

            desk.IsAvailable = false;

            foreach (var date in bookingDto.DaysOfReservation)
            {
                var booking = new Booking() { BookedDay = date, Desk = desk, User = user };
                bookings.Add(booking);
                await _dbContext.Bookings.AddAsync(booking);
            }

            await _dbContext.SaveChangesAsync();
            return bookings;
        }

        public async Task Change(int bookingId, int deskId)
        {
            var desk = await GetDeskById(deskId);
            var booking = await _dbContext.Bookings
                .Include(x => x.Desk)
                .FirstOrDefaultAsync(x => x.Id == bookingId);

            if (DateTimeChecking.IsLessThan24HoursAway(DateTime.Now, booking.BookedDay))
                throw new BadRequestException("Couldn't change the desk - the booked day is less than 24 hours away");

            booking.Desk.IsAvailable = true;
            booking.Desk = desk;

            _dbContext.Bookings.Update(booking);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Desk> GetDeskById(int id)
        {
            var desk = await _dbContext.Desks
                .Include(x => x.Location)
                .Include(x => x.Booking)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (desk is null)
            {
                throw new NotFoundException("Desk not found");
            }
            else
            {
                return desk;
            }
        }
    }
}
