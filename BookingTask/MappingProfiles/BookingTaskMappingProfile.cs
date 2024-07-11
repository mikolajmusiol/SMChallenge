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

            CreateMap<Location, LocationDto>()
                .ReverseMap();

            CreateMap<Desk, DeskDto>();

            CreateMap<Booking, BookingDto>()
                .ForMember(p => p.BookingId, x => x.MapFrom(l => l.Id))
                .ForMember(p => p.UserName, x => x.MapFrom(l => l.User.Name))
                .ForMember(p => p.DeskId, x => x.MapFrom(l => l.Desk.Id))
                .ForMember(p => p.DeskName, x => x.MapFrom(l => l.Desk.Name))
                .ForMember(p => p.BookedDay, x => x.MapFrom(l => l.BookedDay))
                .ForMember(p => p.Country, x => x.MapFrom(l => l.Desk.Location.Country))
                .ForMember(p => p.City, x => x.MapFrom(l => l.Desk.Location.City))
                .ForMember(p => p.Street, x => x.MapFrom(l => l.Desk.Location.Street));
        }
    }
}
