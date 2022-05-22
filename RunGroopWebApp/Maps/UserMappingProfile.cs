using AutoMapper;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Maps
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, IndexUserDto>()
                /*.ForMember(m => m.UserName, c => c.MapFrom(a => a.UserName))
                .ForMember(m => m.Id, c => c.MapFrom(c => c.Id))*/
                //.ForMember(m => _phtService.AddPhotoAsync(m.ImageFile).Result.Url.ToString(), c => c.MapFrom(c => c.Image))
                .ReverseMap();

            CreateMap<User, DetailsUserDto>()
                .ReverseMap();

            CreateMap<User, EditUserDashboardDto>()
                .ReverseMap();
        }
    }
}
