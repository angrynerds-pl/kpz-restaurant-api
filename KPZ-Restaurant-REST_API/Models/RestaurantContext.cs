using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity(typeof(Order), b =>
             {
                 b.HasOne(typeof(Restaurant), "Restaurant")
                    .WithMany()
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.NoAction);
             });

             modelBuilder.Entity(typeof(Product), b =>
             {
                 b.HasOne(typeof(Restaurant), "Restaurant")
                    .WithMany()
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.NoAction);
             });

             modelBuilder.Entity(typeof(Room), b =>
             {
                 b.HasOne(typeof(Restaurant), "Restaurant")
                    .WithMany()
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.NoAction);
             });

             modelBuilder.Entity(typeof(User), b =>
             {
                 b.HasOne(typeof(Restaurant), "Restaurant")
                    .WithMany()
                    .HasForeignKey("RestaurantId")
                    .OnDelete(DeleteBehavior.NoAction);
             });
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInOrder> ProductsInOrders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}
