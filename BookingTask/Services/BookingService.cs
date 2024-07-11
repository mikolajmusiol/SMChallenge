using AutoMapper;
using BookingTask.Exceptions;
using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;
using BookingTask.Utilities;
using DeskBooking.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookingTask.Services
{
    public class BookingService : IBookingService
    {
        private readonly SMCDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ILogger<BookingService> _logger;

        public BookingService(SMCDbContext dbContext, IMapper mapper, 
            IUserContextService userContextService, ILogger<BookingService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userContextService = userContextService;
            _logger = logger;
        }

        public async Task<IEnumerable<BookingDto>> GetBookings()
        {
            var bookings = await _dbContext.Bookings
                .Include(x => x.Desk)
                .Include(x => x.Desk.Location)
                .Include(x => x.User)
                .ToListAsync();
            
            var bookingsDtos = new List<BookingDto>();

            if (_userContextService.User.IsInRole("Admin"))
            {
                bookingsDtos = _mapper.Map<List<BookingDto>>(bookings);
            }
            else
            {
                foreach (var booking in bookings)
                {
                    bookingsDtos.Add(new BookingDto() { DeskId = booking.Desk.Id, BookedDay = booking.BookedDay});
                }
            }

            return bookingsDtos;
        }

        public async Task Book(AddBookingDto bookingDto)
        {
            if (!DateTimeChecking.AreDatesWithinAWeek(bookingDto.DaysOfReservation))
            {
                _logger.LogWarning("Reservation can't exceed a week");
                throw new BadRequestException("Reservation can't exceed a week");
            }
            else if (bookingDto.DaysOfReservation.Min() < DateTime.Now)
            {
                string message = "Invalid date of reservation";
                _logger.LogWarning(message);
                throw new BadRequestException(message);
            }

            var bookings = new List<Booking>();
            var desk = await GetDeskById(bookingDto.DeskId);
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _userContextService.GetUserId);

            if (desk is null)
            {
                _logger.LogError($"Desk id:{desk.Id} not found");
                throw new NotFoundException("Couldn't find desk");
            }
            else if (user is null)
            {
                _logger.LogError($"User with id:{_userContextService.GetUserId} not found");
                throw new NotFoundException("Couldn't find desk");
            }


            if (BookingsOverlap(desk.Bookings, bookingDto.DaysOfReservation))
            {
                _logger.LogError($"Desk id:{desk.Id} is already booked");
                throw new BadRequestException("This desk is already booked");
            }

            desk.IsAvailable = false;

            foreach (var date in bookingDto.DaysOfReservation)
            {
                var booking = new Booking() { BookedDay = date, Desk = desk, User = user };
                bookings.Add(booking);
                await _dbContext.Bookings.AddAsync(booking);
            }
            _dbContext.Desks.Update(desk);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Change(int bookingId, int deskId)
        {
            var desk = await GetDeskById(deskId);
            var booking = await _dbContext.Bookings
                .Include(x => x.Desk)
                .FirstOrDefaultAsync(x => x.Id == bookingId);

            if (DateTimeChecking.IsLessThan24HoursAway(DateTime.Now, booking.BookedDay))
            {
                _logger.LogError($"Couldn't change desk - less than 24h away");
                throw new BadRequestException("Couldn't change the desk - the booked day is less than 24 hours away");
            }

            booking.Desk.IsAvailable = true;
            booking.Desk = desk;

            _dbContext.Bookings.Update(booking);
            _dbContext.Desks.Update(desk);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Desk> GetDeskById(int id)
        {
            var desk = await _dbContext.Desks
                .Include(x => x.Location)
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (desk is null)
            {
                _logger.LogError($"Desk id:{id} not found");
                throw new NotFoundException("Desk not found");
            }
            else
            {
                return desk;
            }
        }

        private bool BookingsOverlap(List<Booking> list1, List<DateTime> list2)
        {
            foreach (var booking in list1)
            {
                if (list2.Contains(booking.BookedDay))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
