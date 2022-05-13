using AutoMapper;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Maps
{
    public class RaceMappingProfile : Profile
    {
        public RaceMappingProfile()
        {
            CreateMap<Race, CreateRaceDto>()
                .ForMember(m => m.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(m => m.City, c => c.MapFrom(c => c.Address.City))
                .ForMember(m => m.State, c => c.MapFrom(c => c.Address.State))
                .ReverseMap();

            CreateMap<Race, EditRaceDto>()
                .ForMember(m => m.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(m => m.City, c => c.MapFrom(c => c.Address.City))
                .ForMember(m => m.State, c => c.MapFrom(c => c.Address.State))
                .ForMember(m => m.Url, c => c.MapFrom(c => c.Image))
                .ReverseMap();
        }
    }
}
