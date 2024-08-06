using System.ComponentModel.DataAnnotations;

namespace BurgerApp.Dto.ViewModels
{
    public class CreateBurgerVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool IsVegan { get; set; }
        [Required]
        public bool IsVegetarian { get; set; }
        [Required]
        public bool HasFries { get; set; }

    }
}
