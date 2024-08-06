using BurgerApp.Domain;

namespace BurgerApp.DataAccess.Interfaces
{
    public interface IOrderBurgerRepository
    {
        IEnumerable<OrderBurger> GetAll();
        OrderBurger GetById(int orderId, int burgerId);
        void Add(OrderBurger entity);
        void Update(OrderBurger entity);
        void Delete(int orderId, int burgerId);
        void UpdateOrderBurgers(int orderId, List<(int BurgerId, int Quantity)> burgerQuantities);
    }
}
