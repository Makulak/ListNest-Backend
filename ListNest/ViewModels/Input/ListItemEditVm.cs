using PotatoServer.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ListNest.ViewModels.Input
{
    public class ListItemEditVm
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [Minimum(0)]
        public float Quantity { get; set; }
    }
}
