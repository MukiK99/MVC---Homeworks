using System.ComponentModel.DataAnnotations;

namespace BurgerApp.Dto.ViewModels
{
    public class CreateLocationVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime OpensAt { get; set; }
        [Required]
        public DateTime ClosesAt { get; set; }
    }
}
