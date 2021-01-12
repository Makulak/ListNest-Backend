using AutoMapper;
using ShoppingListApp.Database.Models;

namespace ShoppingListApp.AutoMapper
{
    public class UserShoppingListProfile : Profile
    {
        public UserShoppingListProfile()
        {
            CreateMap<string, UserShoppingList>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src));
        }
    }
}
