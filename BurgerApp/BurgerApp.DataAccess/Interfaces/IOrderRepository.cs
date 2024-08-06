using BurgerApp.Domain;
using BurgerApp.Dto.Dtos;

namespace BurgerApp.DataAccess.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        BurgerDto GetMostPopularBurger();
        int GetTotalOrderCount();
        double GetAverageOrderPrice();
        bool DeliveredOrder(int id);
    }
}
