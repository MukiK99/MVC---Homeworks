using BurgerApp.Domain;
using BurgerApp.Dto.Dtos;
using System.Runtime.CompilerServices;

namespace BurgerApp.Mapper
{
    public static class MapperExtensions
    {
        public static BurgerDto Map(this Burger burger)
        {
            return new BurgerDto
            {
                Id = burger.Id,
                Name = burger.Name,
                Price = burger.Price,
                IsVegetarian = burger.IsVegetarian,
                IsVegan = burger.IsVegan,
                HasFries = burger.HasFries,
            };
        }
        public static OrderDto Map(this Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                FullName = order.FullName,
                Address = order.Address,
                IsDelivered = order.IsDelivered,
                Location = order.Location, 
                LocationId = order.LocationId,
                OrderBurgers = order.OrderBurgers.Select(ob => new OrderBurgerDto
                {
                    BurgerId = ob.BurgerId,
                    Quantity = ob.Quantity,
                    Burger = ob.Burger 
                }).ToList() 
            };
        }



        public static LocationDto Map(this Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                OpensAt = location.OpensAt,
                ClosesAt = location.ClosesAt
            };
        }
    }
}
