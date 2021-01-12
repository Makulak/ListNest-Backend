using AutoMapper;
using ShoppingListApp.Database.Models;
using ShoppingListApp.ViewModels;

namespace ShoppingListApp.AutoMapper
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, ShoppingListVm>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
