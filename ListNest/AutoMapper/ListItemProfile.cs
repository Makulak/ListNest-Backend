using AutoMapper;
using ListNest.Database.Models;
using ListNest.ViewModels;
using ListNest.ViewModels.Input;

namespace ListNest.AutoMapper
{
    public class ListItemProfile : Profile
    {
        public ListItemProfile()
        {
            CreateMap<ListItem, ListItemVmResult>();
            CreateMap<ListItemCreateVm, ListItem>();
        }
    }
}
