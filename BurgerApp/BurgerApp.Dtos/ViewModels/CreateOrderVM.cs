using BurgerApp.Domain;
using System.ComponentModel.DataAnnotations;

namespace BurgerApp.Dto.ViewModels
{
    public class CreateOrderVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsDelivered { get; set; } = false;
        [Required]
        public int LocationId { get; set; }
        public List<BurgerOrderVM> BurgerOrders { get; set; } = new List<BurgerOrderVM>();
    }
}
