using System.ComponentModel.DataAnnotations;

namespace ListNest.ViewModels.Input
{
    public class ListItemEditVm
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Quantity { get; set; }
    }
}
