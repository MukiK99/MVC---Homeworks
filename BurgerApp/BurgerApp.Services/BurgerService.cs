using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain;
using BurgerApp.Services.Interfaces;
using BurgerApp.Mapper;
using BurgerApp.Dto.ViewModels;
using BurgerApp.Dto.Dtos;
namespace BurgerApp.Services
{
    public class BurgerService : IBurgerService
    {
        private readonly IRepository<Burger> _burgerRepository;
        public BurgerService(IRepository<Burger> burgerRepository)
        {
            _burgerRepository = burgerRepository;
        }
        public BurgerDto GetBurger(int id)
        {
            return _burgerRepository.GetById(id).Map();
        }

        public List<BurgerDto> GetAllBurgers()
        {
            return _burgerRepository.GetAll().Select(x => x.Map()).ToList();
        }
        public void AddBurger(CreateBurgerVM createBurger)
        {
            var newBurger = new Burger
            {
                Name = createBurger.Name,
                Price = createBurger.Price,
                IsVegetarian = createBurger.IsVegetarian,
                IsVegan = createBurger.IsVegan,
                HasFries = createBurger.HasFries
            };
            _burgerRepository.Add(newBurger);
        }

        public void EditBurger(EditBurgerVM editBurger, int burgerId)
        {
            var burger = _burgerRepository.GetById(burgerId);
            if (burger != null)
            {
                burger.Name = editBurger.Name;
                burger.Price = editBurger.Price;
                burger.IsVegetarian = editBurger.IsVegetarian;
                burger.IsVegan = editBurger.IsVegan;
                burger.HasFries = editBurger.HasFries;

                _burgerRepository.Update(burger);

            }
            else
            {
                throw new Exception("Burger not found");
            }


        }


        public void RemoveBurger(int id)
        {
            _burgerRepository.Delete(id);

        }

        
    }
}
