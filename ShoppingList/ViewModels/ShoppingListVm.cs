using System.Collections.Generic;

namespace ShoppingListApp.ViewModels
{
    public class ShoppingListVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserVm> Users { get; set; }
        public ICollection<ShoppingListItemVm> Items { get; set; }
    }
}
