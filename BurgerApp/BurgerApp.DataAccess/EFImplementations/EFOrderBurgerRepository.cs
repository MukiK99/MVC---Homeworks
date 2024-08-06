using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain;

namespace BurgerApp.DataAccess.EFImplementations
{
    public class EFOrderBurgerRepository : IOrderBurgerRepository
    {
        private readonly BurgerAppDbContext _context;
        public EFOrderBurgerRepository(BurgerAppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<OrderBurger> GetAll()
        {
            return _context.OrderBurgers.ToList();
        }

        public OrderBurger GetById(int orderId, int burgerId)
        {
            return _context.OrderBurgers
                      .FirstOrDefault(ob => ob.OrderId == orderId && ob.BurgerId == burgerId);
        }
        public void Add(OrderBurger entity)
        {
            _context.OrderBurgers.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int orderId, int burgerId)
        {
            var orderBurger = GetById(orderId, burgerId);
            if (orderBurger != null)
            {
                _context.OrderBurgers.Remove(orderBurger);
                _context.SaveChanges();
            }
        }


        public void Update(OrderBurger entity)
        {
            var existingOrderBurger = GetById(entity.OrderId, entity.BurgerId);
            if (existingOrderBurger != null)
            {
                existingOrderBurger.Quantity = entity.Quantity;
                _context.SaveChanges();
            }
        }

        public void UpdateOrderBurgers(int orderId, List<(int BurgerId, int Quantity)> burgerQuantities)
        {
            var existingOrderBurgers = _context.OrderBurgers
                                               .Where(ob => ob.OrderId == orderId)
                                               .ToList();
            _context.OrderBurgers.RemoveRange(existingOrderBurgers);

            foreach (var (burgerId, quantity) in burgerQuantities)
            {
                _context.OrderBurgers.Add(new OrderBurger
                {
                    OrderId = orderId,
                    BurgerId = burgerId,
                    Quantity = quantity
                });
            }

            _context.SaveChanges();
        }
    }
}
