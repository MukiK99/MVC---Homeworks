namespace BurgerApp.Domain
{
    public class Order : BaseEntity
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsDelivered { get; set; }
        //public List<Burger> Burgers { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public List<OrderBurger> OrderBurgers { get; set; }
    }
}
