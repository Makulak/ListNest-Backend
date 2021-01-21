using System.ComponentModel.DataAnnotations;

namespace ListNest.ViewModels.Input
{
    public class ListCreateVm
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public string[] UserIds { get; set; }
    }
}
