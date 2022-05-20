using AutoMapper;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp
{
    public class ClubMappingProfile : Profile
    {
        //private readonly IPhotoService _phtService;

        public ClubMappingProfile(/*IPhotoService phtService*/)
        {
            //_phtService = phtService;
            CreateMap<Club, CreateClubDto>()
                .ForMember(m => m.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(m => m.City, c => c.MapFrom(c => c.Address.City))
                .ForMember(m => m.State, c => c.MapFrom(c => c.Address.State))
                //.ForMember(m => _phtService.AddPhotoAsync(m.ImageFile).Result.Url.ToString(), c => c.MapFrom(c => c.Image))
                .ReverseMap();

            CreateMap<Club, EditClubDto>()
                .ForMember(m => m.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(m => m.City, c => c.MapFrom(c => c.Address.City))
                .ForMember(m => m.State, c => c.MapFrom(c => c.Address.State))
                .ForMember(m => m.Url, c => c.MapFrom(c => c.Image))
                .ReverseMap()
                /*.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null))*/;
        }
    }
}