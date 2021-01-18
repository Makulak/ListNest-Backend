using AutoMapper;
using ListNest.Database.Models;
using ListNest.ViewModels;

namespace ListNest.AutoMapper
{
    public class UserListProfile : Profile
    {
        public UserListProfile()
        {
            CreateMap<string, UserList>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src));

            CreateMap<UserList, UserVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
