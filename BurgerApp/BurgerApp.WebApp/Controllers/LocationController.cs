using BurgerApp.Dto.ViewModels;
using BurgerApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BurgerApp.WebApp.Controllers
{
    [Route("location")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var locations = _locationService.GetAllLocations();
            return View(locations);
        }
        [HttpPost("remove")]
        public IActionResult Remove(int id)
        {
            _locationService.RemoveLocation(id);
            return RedirectToAction("Index");
        }
        [HttpGet("add")]
        public IActionResult AddLocation()
        {
            return View("AddLocation");
        }
        [HttpPost("add")]
        public IActionResult AddLocation(CreateLocationVM createLocationVM)
        {
            _locationService.AddLocation(createLocationVM);
            return RedirectToAction("Index");
        }
        [HttpGet("edit/{id}")]
        public IActionResult EditLocation(int id)
        {
            var location = _locationService.GetLocation(id);
            if(location == null)
            {
                return NotFound();
            }
            var editLocation = new EditLocationVM
            {
                Id = location.Id
            };
            return View(editLocation);
        }
        [HttpPost("edit/{id}")]
        public IActionResult EditLocation(EditLocationVM editLocationVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _locationService.EditLocation(editLocationVM, editLocationVM.Id);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the location.");

                }
            }
            return View("EditLocation", editLocationVM);
        }
    }
}
