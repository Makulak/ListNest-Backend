using System.ComponentModel.DataAnnotations;

namespace ListNest.ViewModels.Input
{
    public class ListCreateVm
    {
        [Required]
        public string Name { get; set; }

        public string[] UserIds { get; set; }
    }
}
