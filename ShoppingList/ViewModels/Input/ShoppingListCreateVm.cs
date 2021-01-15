using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.ViewModels.Input
{
    public class ShoppingListCreateVm
    {
        [Required]
        public string Name { get; set; }

        public string[] UserIds { get; set; }
    }
}
