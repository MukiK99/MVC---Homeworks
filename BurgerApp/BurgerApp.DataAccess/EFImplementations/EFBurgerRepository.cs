using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain;

namespace BurgerApp.DataAccess.EFImplementations
{
    public class EFBurgerRepository : IRepository<Burger>
    {
        private readonly BurgerAppDbContext _context;

        public EFBurgerRepository(BurgerAppDbContext context)
        {
            _context = context;
        }

        public EFBurgerRepository() { }

        public IEnumerable<Burger> GetAll()
        {
            return _context.Burger.ToList();
        }

        public Burger GetById(int id)
        {
            return _context.Burger.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Burger entity)
        {
            _context.Burger.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Burger entity)
        {
            _context.Burger.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var burgerDb = GetById(id);
            if (burgerDb != null)
            {
                _context.Burger.Remove(burgerDb);
                _context.SaveChanges();
            }
        }
    }
}
