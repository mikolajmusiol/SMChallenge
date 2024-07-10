using AutoMapper;
using BookingTask.Models.DTOs;
using DeskBooking.Entities;


namespace DeskBooking.MappingProfiles
{
    public class BookingTaskMappingProfile : Profile
    {
        public BookingTaskMappingProfile()
        {
            CreateMap<AddLocationDto, Location>();
        }
    }
}
