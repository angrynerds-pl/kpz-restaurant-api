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
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Category>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<Order>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<OrderedProducts>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<Product>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<Reservation>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<Room>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<Table>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<User>().HasQueryFilter(c => c.DeletedAt == null);
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderedProducts> OrderedProducts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        //STATISTICS 
        public DbSet<IncomeByMonth> IncomeByMonth { get; set; }

    }
}
