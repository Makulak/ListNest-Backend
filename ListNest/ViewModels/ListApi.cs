using System.Collections.Generic;

namespace ListNest.ViewModels
{
    public class ListApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserApi> Users { get; set; }
        public ICollection<ListItemApi> Items { get; set; }
    }
}
