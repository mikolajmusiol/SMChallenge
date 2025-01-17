﻿using AutoMapper;
using BookingTask.Exceptions;
using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;
using DeskBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingTask.Services
{
    public class DeskService : IDeskService
    {
        private readonly SMCDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<DeskService> _logger;

        public DeskService(SMCDbContext dbContext, IMapper mapper, ILogger<DeskService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DeskDto>> GetDesks(string? location, DateTime? date)
        {
            var desks = await _dbContext.Desks
                .Include(x => x.Location)
                .Include(x => x.Bookings)
                .ToListAsync();

            if (date is not null)
                desks = desks.Where(x => x.Bookings.Any(d => d.BookedDay.Date == date.Value.Date)).ToList();

            if (location is not null)
            {
                desks = desks.Where(
                    x => location == null ||
                    x.Location.Country.ToLower().Contains(location.ToLower()) ||
                    x.Location.City.ToLower().Contains(location.ToLower()) ||
                    x.Location.Street.ToLower().Contains(location.ToLower()))
                    .ToList(); 
            }

            var desksDtos = _mapper.Map<List<DeskDto>>(desks);
            return desksDtos;
        }

        public async Task<int> Add(AddDeskDto deskDto)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == deskDto.LocationId);

            if (location is null)
            {
                _logger.LogError($"Wrong location");
                throw new NotFoundException("Wrong location");
            }

            var presentDesk = await _dbContext.Desks.AnyAsync(x => x.Name == deskDto.Name);
            if (presentDesk)
            {
                var message = $"Desk Name not Unique: {deskDto.Name}";
                _logger.LogError(message);
                throw new NotFoundException(message);
            }

            var desk = new Desk() { Location = location, Name = deskDto.Name };
            await _dbContext.Desks.AddAsync(desk);
            await _dbContext.SaveChangesAsync();
            return desk.Id;
        }

        public async Task Delete(int id)
        {
            var desk = await GetDeskById(id);

            if (desk.IsAvailable)
            {
                _dbContext.Desks.Remove(desk);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogError($"Atempt of deletion of desk id: {desk.Id}");
                throw new BadRequestException("Couldn't delete reserved desk");
            }
        }

        public async Task MakeUnavailable(int id)
        {
            var desk = await GetDeskById(id);

            if (desk.IsAvailable)
            {
                desk.IsAvailable = false;
                _dbContext.Desks.Update(desk);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Desk id:{id} is already unavailable");
                throw new BadRequestException("This desk is already unavailable");
            }
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
    }
}
