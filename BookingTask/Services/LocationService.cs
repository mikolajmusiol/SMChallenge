using AutoMapper;
using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;
using DeskBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingTask.Services
{
    public class LocationService : ILocationService
    {
        private readonly SMCDbContext _dbContext;
        private readonly IMapper _mapper;

        public LocationService(SMCDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Add(AddLocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            await _dbContext.Locations.AddAsync(location);
            await _dbContext.SaveChangesAsync();
            return location.Id;
        }

        public async Task Delete(int id)
        {
            var location = await _dbContext.Locations.FirstOrDefaultAsync(x => x.Id == id);

            if (location is not null && !location.Desks.Any())
            {
                _dbContext.Locations.Remove(location);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Location>> GetLocations()
        {
            var locations = await _dbContext.Locations.Include(x => x.Desks).ToListAsync();
            return locations;
        }
    }
}
