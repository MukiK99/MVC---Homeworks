using BurgerApp.Dto.ViewModels;
using BurgerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.WebApp.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;
        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            var homeVM = new HomeVM
            {
                MostPopularBurger = _orderService.MostPopularBurgerInTheMenu(),
                NumberOrOrders = _orderService.NumberOfOrders(),
                AverageOrderPrice = _orderService.AverageOrderPrice()
            };
            return View(homeVM);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
