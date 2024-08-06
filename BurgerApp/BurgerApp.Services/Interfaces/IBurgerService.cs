using BurgerApp.Dto.Dtos;
using BurgerApp.Dto.ViewModels;

namespace BurgerApp.Services.Interfaces
{
    public interface IBurgerService
    {
        List<BurgerDto> GetAllBurgers();
        void EditBurger(EditBurgerVM editBurger, int burgerId);
        BurgerDto GetBurger(int id);

        void RemoveBurger(int id);
        void AddBurger(CreateBurgerVM createBurger);
    }
}
