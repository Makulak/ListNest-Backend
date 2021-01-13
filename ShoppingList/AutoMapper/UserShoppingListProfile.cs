using AutoMapper;
using ShoppingListApp.Database.Models;
using ShoppingListApp.ViewModels;

namespace ShoppingListApp.AutoMapper
{
    public class UserShoppingListProfile : Profile
    {
        public UserShoppingListProfile()
        {
            CreateMap<string, UserShoppingList>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src));

            CreateMap<UserShoppingList, UserVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
