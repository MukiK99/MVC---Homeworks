using BurgerApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerApp.DataAccess
{
    public class BurgerAppDbContext : DbContext
    {
        public DbSet<Burger> Burger { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<OrderBurger> OrderBurgers { get; set; }


        public BurgerAppDbContext() { }
        public BurgerAppDbContext(DbContextOptions<BurgerAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderBurger>()
         .HasKey(ob => new { ob.OrderId, ob.BurgerId });

            modelBuilder.Entity<OrderBurger>()
                .Property(ob => ob.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderBurgers)
                .WithOne(ob => ob.Order)
                .HasForeignKey(ob => ob.OrderId);

            modelBuilder.Entity<Burger>()
                .HasMany(b => b.OrderBurgers)
                .WithOne(ob => ob.Burger)
                .HasForeignKey(ob => ob.BurgerId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Location)
                .WithMany(l => l.Orders)
                .HasForeignKey(o => o.LocationId);

            // SEED BURGERS

            modelBuilder.Entity<Burger>()
                .HasData(new Burger
                {
                    Id = 1,
                    Name = "Hamburger",
                    Price = 2.99,
                    IsVegetarian = false,
                    IsVegan = false,
                    HasFries = true

                });

            modelBuilder.Entity<Burger>()
               .HasData(new Burger
               {
                   Id = 2,
                   Name = "Cheeseburger",
                   Price = 3.70,
                   IsVegetarian = false,
                   IsVegan = false,
                   HasFries = true

               });

            modelBuilder.Entity<Burger>()
                   .HasData(new Burger
                   {
                       Id = 3,
                       Name = "Chickenburger",
                       Price = 3.80,
                       IsVegetarian = false,
                       IsVegan = false,
                       HasFries = true

                   });

            modelBuilder.Entity<Burger>()
               .HasData(new Burger
               {
                   Id = 4,
                   Name = "Veggieburger",
                   Price = 2.70,
                   IsVegetarian = true,
                   IsVegan = false,
                   HasFries = false

               });

            modelBuilder.Entity<Burger>()
               .HasData(new Burger
               {
                   Id = 5,
                   Name = "Veganburger",
                   Price = 1.90,
                   IsVegetarian = true,
                   IsVegan = true,
                   HasFries = false

               });

            // SEED ORDERS

            modelBuilder.Entity<Order>()
                .HasData(new Order
                {
                    Id = 1,
                    FullName = "Murat Koca",
                    Address = "Partizanski Odredi BB",
                    IsDelivered = true,
                    LocationId = 1,

                });

            modelBuilder.Entity<Order>()
                .HasData(new Order
                {
                    Id = 2,
                    FullName = "Radica Shvigir",
                    Address = "Ilindenska BB",
                    IsDelivered = true,
                    LocationId = 1,
                });

            modelBuilder.Entity<Order>()
                .HasData(new Order
                {
                    Id = 3,
                    FullName = "Jack Man",
                    Address = "Guangzhou 11",
                    IsDelivered = false,
                    LocationId = 1,
                });

            modelBuilder.Entity<Order>()
                .HasData(new Order
                {
                    Id = 4,
                    FullName = "John Legends",
                    Address = "Manhattan 23",
                    IsDelivered = false,
                    LocationId = 2,
                });

            // Seed OrderBurgers (Relationships)
            modelBuilder.Entity<OrderBurger>().HasData(
                new OrderBurger
                {
                    Id = 100,
                    OrderId = 1,
                    BurgerId = 3,
                    Quantity = 2
                },
                new OrderBurger
                {
                    Id = 101,
                    OrderId = 1,
                    BurgerId = 4,
                    Quantity = 4
                },
                new OrderBurger
                {
                    Id = 102,
                    OrderId = 2,
                    BurgerId = 1,
                    Quantity = 3
                },
                new OrderBurger
                {
                    Id = 103,
                    OrderId = 3,
                    BurgerId = 2,
                    Quantity = 3,
                },
                new OrderBurger
                {
                    Id = 104,
                    OrderId = 3,
                    BurgerId = 3,
                    Quantity = 6
                },
            new OrderBurger
            {
                Id = 105,
                OrderId = 4,
                BurgerId = 5,
                Quantity = 1,
            });



            // SEED LOCATIONS

            modelBuilder.Entity<Location>()
                .HasData(new Location
                {
                    Id = 1,
                    Name = "MacShop",
                    Address = "ul.Makedonija",
                    OpensAt = DateTime.Today.AddHours(8),
                    ClosesAt = DateTime.Today.AddHours(22)
                });

            modelBuilder.Entity<Location>()
               .HasData(new Location
               {
                   Id = 2,
                   Name = "ChinaShop",
                   Address = "Chinatown",
                   OpensAt = DateTime.Today.AddHours(8),
                   ClosesAt = DateTime.Today.AddHours(24)
               });



            base.OnModelCreating(modelBuilder);


        }
    }
}
