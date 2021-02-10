using AutoMapper;
using PotatoServer.Database.Models;
using ListNest.ViewModels;

namespace ListNest.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVmResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
