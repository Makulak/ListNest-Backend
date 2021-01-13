using AutoMapper;
using ShoppingListApp.Database.Models;
using ShoppingListApp.ViewModels;

namespace ShoppingListApp.AutoMapper
{
    public class ShoppingListItemProfile : Profile
    {
        public ShoppingListItemProfile()
        {
            CreateMap<ShoppingListItem, ShoppingListItemVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
