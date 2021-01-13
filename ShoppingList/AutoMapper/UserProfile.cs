using AutoMapper;
using PotatoServer.Database.Models;
using ShoppingListApp.ViewModels;

namespace ShoppingListApp.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
