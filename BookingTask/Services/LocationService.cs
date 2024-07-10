using AutoMapper;
using BookingTask.Models.DTOs;
using BookingTask.Services.Interfaces;
using DeskBooking.Entities;

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
    }
}
