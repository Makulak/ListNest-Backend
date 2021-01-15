using AutoMapper;
using ShoppingListApp.Database.Models;
using ShoppingListApp.ViewModels;
using ShoppingListApp.ViewModels.Input;

namespace ShoppingListApp.AutoMapper
{
    public class ShoppingListProfile : Profile
    {
        public ShoppingListProfile()
        {
            CreateMap<ShoppingList, ShoppingListVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));

            CreateMap<ShoppingListCreateVm, ShoppingList>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.UserIds));
        }
    }
}
