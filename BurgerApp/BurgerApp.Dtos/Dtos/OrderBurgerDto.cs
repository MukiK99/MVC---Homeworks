using BurgerApp.Domain;

namespace BurgerApp.Dto.Dtos
{
    public class OrderBurgerDto
    {
        public int Id { get; set; }
        public Burger Burger { get; set; }
        public int BurgerId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
