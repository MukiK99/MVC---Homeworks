using BurgerApp.Dto.Dtos;

namespace BurgerApp.Dto.ViewModels
{
    public class HomeVM
    {
        public BurgerDto MostPopularBurger { get; set; }
        public int NumberOrOrders { get; set; }
        public double AverageOrderPrice { get; set; }
    }
}
