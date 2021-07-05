using AutoMapper;
using ListNest.Database.Models;
using ListNest.ViewModels;
using ListNest.ViewModels.Input;

namespace ListNest.AutoMapper
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<List, ListApi>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.AssignedUsers));

            CreateMap<ListCreateVm, List>()
                .ForMember(dest => dest.AssignedUsers, opt => opt.MapFrom(src => src.UserIds));
        }
    }
}
