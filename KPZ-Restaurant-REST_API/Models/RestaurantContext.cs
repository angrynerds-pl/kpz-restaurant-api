using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Models
{
    public class RestaurantContext: DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductInOrder> ProductsInOrders{ get; set; }
        public DbSet<Reservation> Reservations{ get; set; }
        public DbSet<Room> Rooms{ get; set; }
        public DbSet<Table> Tables{ get; set; }
    }
}
