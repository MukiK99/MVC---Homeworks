using BurgerApp.Dto.ViewModels;
using BurgerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.WebApp.Controllers
{
    [Route("")]
    public class BurgerController : Controller
    {
        private readonly IBurgerService _burgerService;

        public BurgerController(IBurgerService burgerService)
        {
            _burgerService = burgerService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var burgers = _burgerService.GetAllBurgers();
            return View(burgers);
        }
        [HttpPost("remove")]
        public IActionResult Remove(int id)
        {
            _burgerService.RemoveBurger(id);
            return RedirectToAction("Index");
        }
        [HttpGet("add")]
        public IActionResult AddBurger()
        {
            return View("AddBurger");
        }
        [HttpPost("add")]
        public IActionResult AddBurger(CreateBurgerVM createBurgerVM)
        {
            _burgerService.AddBurger(createBurgerVM);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult EditBurger(int id)
        {
            var burger = _burgerService.GetBurger(id);
            if (burger == null)
            {
                return NotFound(); 
            }

            var editBurgerVM = new EditBurgerVM
            {
                Id = burger.Id,
            };

            return View(editBurgerVM);
        }



        [HttpPost("edit/{id}")]
        public IActionResult EditBurger(EditBurgerVM editBurgerVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _burgerService.EditBurger(editBurgerVM, editBurgerVM.Id);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "An error occurred while updating the burger.");
                }
            }

            return View("EditBurger", editBurgerVM);
        }
    }
}
