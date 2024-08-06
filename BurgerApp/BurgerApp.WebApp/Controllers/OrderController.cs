using BurgerApp.Dto.ViewModels;
using BurgerApp.Services;
using BurgerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BurgerApp.WebApp.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILocationService _locationService;
        private readonly IBurgerService _burgerService;
        public OrderController(IOrderService orderService, ILocationService locationService,IBurgerService burgerService)
        {
            _orderService = orderService;
            _locationService = locationService;
            _burgerService = burgerService;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }
        [HttpPost("remove")]
        public IActionResult Remove(int id)
        {
            _orderService.RemoveOrder(id);
            return RedirectToAction("Index");
        }
        [HttpPost("deliver")]
        public IActionResult OrderDelivered(int id)
        {
            _orderService.OrderIsDelivered(id);
            return RedirectToAction("Index");
        }

        [HttpGet("add")]
        public IActionResult AddOrder()
        {
            var locations = _locationService.GetAllLocations();
            var burgers = _burgerService.GetAllBurgers();

            ViewBag.Locations = locations.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();

            ViewBag.Burgers = burgers.Select(b => new BurgerOrderVM
            {
                BurgerId = b.Id,
                BurgerName = b.Name,
                Quantity = 0
            }).ToList();


            return View(new CreateOrderVM());
        }



        [HttpPost("add")]
        public IActionResult AddOrder(CreateOrderVM createOrderVM)
        {
            if (ModelState.IsValid)
            {
                _orderService.AddOrder(createOrderVM);
                return RedirectToAction("Index");
            }
            foreach (var modelState in ModelState)
            {
                var key = modelState.Key;
                var errors = modelState.Value.Errors;

                foreach (var error in errors)
                {
                    Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                }
            }
            var locations = _locationService.GetAllLocations();
            var burgers = _burgerService.GetAllBurgers();

            ViewBag.Locations = locations.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();

            ViewBag.Burgers = burgers.Select(b => new BurgerOrderVM
            {
                BurgerId = b.Id,
                BurgerName = b.Name,
                Quantity = createOrderVM.BurgerOrders.FirstOrDefault(bo => bo.BurgerId == b.Id)?.Quantity ?? 0
            }).ToList();

            return View(createOrderVM);
        }

        [HttpGet("edit/{id}")]
        public IActionResult EditOrder(int id)
        {
            var locations = _locationService.GetAllLocations();
            var order = _orderService.GetOrder(id);
            var burgers = _burgerService.GetAllBurgers();

            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Locations = locations.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = l.Name
            }).ToList();
            ViewBag.Burgers = burgers.Select(b => new BurgerOrderVM
            {
                BurgerId = b.Id,
                BurgerName = b.Name,
                Quantity = 0
            }).ToList();
            var editOrderVM = new EditOrderVM
            {
                Id = order.Id,
            };
            return View(editOrderVM);
        }
        [HttpPost("edit/{id}")]
        public IActionResult EditOrder(EditOrderVM editOrderVM)
        {
            var locations = _locationService.GetAllLocations();
            var burgers = _burgerService.GetAllBurgers();
            if (ModelState.IsValid)
            {
                try
                {
                    _orderService.EditOrder(editOrderVM, editOrderVM.Id);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "An error occurred while updating the order.");
                }
            }

            ViewBag.Locations = _locationService.GetAllLocations()
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList();
            ViewBag.Burgers = burgers.Select(b => new BurgerOrderVM
            {
                BurgerId = b.Id,
                BurgerName = b.Name,
                Quantity = editOrderVM.BurgerOrders.FirstOrDefault(bo => bo.BurgerId == b.Id)?.Quantity ?? 0
            }).ToList();
            return View("EditOrder", editOrderVM);
        }
    }
}
