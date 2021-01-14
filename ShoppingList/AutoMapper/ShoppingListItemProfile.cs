using AutoMapper;
using ShoppingListApp.Database.Models;
using ShoppingListApp.ViewModels;
using ShoppingListApp.ViewModels.Input;

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

            CreateMap<ShoppingListItemInputVm, ShoppingListItem>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}
