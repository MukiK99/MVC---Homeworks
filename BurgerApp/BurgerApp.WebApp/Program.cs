using BurgerApp.DataAccess;
using BurgerApp.DataAccess.EFImplementations;
using BurgerApp.DataAccess.Interfaces;
using BurgerApp.Domain;
using BurgerApp.Services;
using BurgerApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BurgerApp.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Register Database
            string connectionString = builder.Configuration.GetConnectionString("BurgerAppConnectionString");

            builder.Services.AddDbContext<BurgerAppDbContext>(options => options.UseSqlServer(connectionString));

            #endregion

            #region Register Repositories
            builder.Services.AddTransient<IRepository<Burger>, EFBurgerRepository>();
            builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
            builder.Services.AddTransient<IRepository<Location>, EFLocationRepository>();
            builder.Services.AddTransient<IOrderBurgerRepository, EFOrderBurgerRepository>();


            #endregion

            #region Register Services
            builder.Services.AddTransient<IBurgerService, BurgerService>();
            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<ILocationService, LocationService>();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Burger}/{action=Index}/{id?}");

            app.Run();
        }
    }
}