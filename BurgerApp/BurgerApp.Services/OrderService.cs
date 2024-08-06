using BurgerApp.DataAccess.EFImplementations;
using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain;
using BurgerApp.Dto.Dtos;
using BurgerApp.Dto.ViewModels;
using BurgerApp.Mapper;
using BurgerApp.Services.Interfaces;

namespace BurgerApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderBurgerRepository _orderBurgerRepository;

        public OrderService(IOrderRepository orderRepository, IOrderBurgerRepository orderBurgerRepository)
        {
            _orderRepository = orderRepository;
            _orderBurgerRepository = orderBurgerRepository;
        }

        public OrderDto GetOrder(int orderId)
        {
            return _orderRepository.GetById(orderId).Map();
        }
        public List<OrderDto> GetAllOrders()
        {
            return _orderRepository.GetAll().Select(x => x.Map()).ToList();
        }

        public void AddOrder(CreateOrderVM createOrder)
        {
            var newOrder = new Order
            {
                FullName = createOrder.FullName,
                Address = createOrder.Address,
                IsDelivered = createOrder.IsDelivered,
                LocationId = createOrder.LocationId
            };

            var orderBurgers = createOrder.BurgerOrders
                .Where(bo => bo.Quantity > 0)
                .Select(bo => new OrderBurger
                {
                    OrderId = newOrder.Id,
                    BurgerId = bo.BurgerId,
                    Quantity = bo.Quantity
                }).ToList();

            newOrder.OrderBurgers = orderBurgers;
            _orderRepository.Add(newOrder); 
        }


        public void EditOrder(EditOrderVM editOrder, int orderId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order != null)
            {
                order.FullName = editOrder.FullName;
                order.Address = editOrder.Address;
                order.IsDelivered = editOrder.IsDelivered;
                order.LocationId = editOrder.LocationId;

                _orderRepository.Update(order);

                var burgerQuantities = editOrder.BurgerOrders
                    .Where(bo => bo.Quantity > 0)
                    .Select(bo => (BurgerId: bo.BurgerId, Quantity: bo.Quantity))
                    .ToList();

                _orderBurgerRepository.UpdateOrderBurgers(orderId, burgerQuantities);
            }
            else
            {
                throw new Exception("Order not found");
            }
        }


        public void RemoveOrder(int id)
        {
            _orderRepository.Delete(id);
        }

        public BurgerDto MostPopularBurgerInTheMenu()
        {
            return _orderRepository.GetMostPopularBurger();
        }

        public int NumberOfOrders()
        {
            return _orderRepository.GetTotalOrderCount();
        }

        public double AverageOrderPrice()
        {
            return _orderRepository.GetAverageOrderPrice();
        }
        public bool OrderIsDelivered(int id)
        {
            return _orderRepository.DeliveredOrder(id);
        }
    }
}
