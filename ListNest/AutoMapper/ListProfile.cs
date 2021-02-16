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
            CreateMap<List, ListVmResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.AssignedUsers));

            CreateMap<ListCreateVm, List>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AssignedUsers, opt => opt.MapFrom(src => src.UserIds));
        }
    }
}
