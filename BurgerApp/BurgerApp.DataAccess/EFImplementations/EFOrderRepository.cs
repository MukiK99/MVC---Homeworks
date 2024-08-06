using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain;
using Microsoft.EntityFrameworkCore;
using BurgerApp.Mapper;
using BurgerApp.Dto.Dtos;

namespace BurgerApp.DataAccess.EFImplementations
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly BurgerAppDbContext _context;

        public EFOrderRepository(BurgerAppDbContext context)
        {
            _context = context;
        }
        public EFOrderRepository() { }

        public IEnumerable<Order> GetAll()
        {
            return _context.Order
                .Include(x => x.Location)
                .Include(x => x.OrderBurgers)
                .ThenInclude(ob => ob.Burger)
                .ToList();
        }

        public Order GetById(int id)
        {
            return _context.Order
                 .Include(x => x.Location)
                 .Include(x => x.OrderBurgers)
                 .ThenInclude(ob => ob.Burger)
                 .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Order entity)
        {
            _context.Order.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Order entity)
        {
            _context.Order.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var orderDb = GetById(id);
            if (orderDb != null)
            {
                _context.Order.Remove(orderDb);
                _context.SaveChanges();
            }
        }

        public BurgerDto GetMostPopularBurger()
        {
            var mostPopularBurger = _context.OrderBurgers
                .GroupBy(ob => ob.BurgerId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            var burger = _context.Burger.Find(mostPopularBurger);

            return burger.Map() ?? new BurgerDto { Name = "No burgers ordered!" };


        }

        public int GetTotalOrderCount()
        {
            return _context.Order.Count();
        }

        public double GetAverageOrderPrice()
        {
            var orderPrices = _context.Order
                .Select(o => o.OrderBurgers.Sum(ob => ob.Quantity * ob.Burger.Price))
                .ToList();

            if (!orderPrices.Any())
            {
                return 0;
            }

            return orderPrices.Average();
        }
        public bool DeliveredOrder(int id)
        {
            var order = GetById(id);
            if(order != null)
            {
                order.IsDelivered = true;
                Update(order);
                return true;
            }
            return false;
            
        }
    }

}

