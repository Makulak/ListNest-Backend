using System.Collections.Generic;

namespace ListNest.ViewModels
{
    public class ListVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserVm> Users { get; set; }
        public ICollection<ListItemVm> Items { get; set; }
    }
}
