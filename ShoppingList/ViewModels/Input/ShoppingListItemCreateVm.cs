using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.ViewModels.Input
{
    public class ShoppingListItemCreateVm
    {
        [Required]
        public int ShoppingListId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Quantity { get; set; }
    }
}
