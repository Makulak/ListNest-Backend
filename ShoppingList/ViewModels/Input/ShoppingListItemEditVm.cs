using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.ViewModels.Input
{
    public class ShoppingListItemEditVm
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Quantity { get; set; }
    }
}
