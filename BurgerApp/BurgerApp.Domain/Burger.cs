namespace BurgerApp.Domain
{
    public class Burger : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan {  get; set; }
        public bool HasFries { get; set; }
        public List<OrderBurger> OrderBurgers { get; set; }

    }
}
