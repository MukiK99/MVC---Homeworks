namespace BurgerApp.Domain
{
    public class OrderBurger : BaseEntity
    {
        public Burger Burger { get; set; }
        public int BurgerId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
