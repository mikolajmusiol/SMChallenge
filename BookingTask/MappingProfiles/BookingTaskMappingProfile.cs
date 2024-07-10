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
            CreateMap<Desk, DeskDto>()
                .ForMember(p => p.Country, x => x.MapFrom(l => l.Location.Country))
                .ForMember(p => p.City, x => x.MapFrom(l => l.Location.City))
                .ForMember(p => p.Street, x => x.MapFrom(l => l.Location.Street))
                .ForMember(p => p.BookedDay, x => x.MapFrom(b => b.Booking.BookedDay));
            CreateMap<AddDeskDto, Desk>()
                .ForPath(p => p.Location.Country, x => x.MapFrom(l => l.Country))
                .ForPath(p => p.Location.City, x => x.MapFrom(l => l.City))
                .ForPath(p => p.Location.Street, x => x.MapFrom(l => l.Street));
        }
    }
}
