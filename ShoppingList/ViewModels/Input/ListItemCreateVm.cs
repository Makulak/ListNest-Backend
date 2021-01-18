using System.ComponentModel.DataAnnotations;

namespace ListNest.ViewModels.Input
{
    public class ListItemCreateVm
    {
        [Required]
        public int ListId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Quantity { get; set; }
    }
}
