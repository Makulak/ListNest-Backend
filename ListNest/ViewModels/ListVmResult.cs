using System.Collections.Generic;

namespace ListNest.ViewModels
{
    public class ListVmResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserVmResult> Users { get; set; }
        public ICollection<ListItemVmResult> Items { get; set; }
    }
}
