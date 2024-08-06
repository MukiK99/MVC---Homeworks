using BurgerApp.Domain;
using BurgerApp.Dto.Dtos;
using BurgerApp.Dto.ViewModels;

namespace BurgerApp.Services.Interfaces
{
    public interface IOrderService
    {
        List<OrderDto> GetAllOrders();
        void EditOrder(EditOrderVM editOrder, int orderId);
        OrderDto GetOrder(int orderId);
        void RemoveOrder(int id);
        void AddOrder(CreateOrderVM createOrder);
        BurgerDto MostPopularBurgerInTheMenu();
        int NumberOfOrders();
        double AverageOrderPrice();
        bool OrderIsDelivered(int id);
    }
}
