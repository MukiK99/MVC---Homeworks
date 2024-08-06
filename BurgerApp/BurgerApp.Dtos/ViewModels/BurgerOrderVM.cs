using System.ComponentModel.DataAnnotations;

namespace BurgerApp.Dto.ViewModels
{
    public class BurgerOrderVM
    {
        public int BurgerId { get; set; }
        public string BurgerName { get; set; }
        public int Quantity { get; set; }
    }
}
