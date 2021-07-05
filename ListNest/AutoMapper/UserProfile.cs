using AutoMapper;
using ListNest.Database.Models;
using ListNest.ViewModels;

namespace ListNest.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ListNestUser, UserApi>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
