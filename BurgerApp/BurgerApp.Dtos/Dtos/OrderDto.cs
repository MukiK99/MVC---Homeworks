using BurgerApp.Domain;
using System;

namespace BurgerApp.Dto.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool IsDelivered { get; set; }
        //public List<Burger> Burgers { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
        public List<OrderBurgerDto> OrderBurgers { get; set; }

    }
}
