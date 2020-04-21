using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KPZ_Restaurant_REST_API.Models
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<RestaurantContext>());
            }

        }
        public static void SeedData(RestaurantContext context)
        {
            System.Console.WriteLine("Appling Migrations...");

            context.Database.EnsureDeleted();
            context.Database.Migrate();


            var mcdonalds = new Restaurant() { Name = "McDonalds" };

            //Add restaurants
            if (!context.Restaurants.Any())
            {
                context.Restaurants.AddRange(
                   mcdonalds
               );
            context.SaveChanges();

            }

            //Add rooms
            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange(
                   new Room() { RestaurantId = mcdonalds.Id, Name = "Sala 1", Rows = 3, Columns = 3 }
               );
            context.SaveChanges();

            }

            //Add tables
            if (!context.Tables.Any())
            {
                context.Tables.AddRange(
                   new Table()
                   {
                       Number = 1,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 0,
                       Y = 0
                   }
               );

            }

            //Add categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new Category()
                {
                    RestaurantId = mcdonalds.Id,
                    Name = "Burgers"
                }
               );
            context.SaveChanges();

            }

            //Add product
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Big Mac", Price = 12.99M, CategoryId = context.Categories.FirstOrDefault().Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "McRoyal", Price = 13.99M, CategoryId = context.Categories.FirstOrDefault().Id }
               );

            context.SaveChanges();

            }


            //Add users
            if (!context.Users.Any())
            {
                //Seed data
                Console.WriteLine("Adding data to User table");
                context.Users.AddRange(
                   new User() { FirstName = "Janusz", LastName = "Biernat", Username = "jbiernat", Password = "architektura", Rights = UserType.WAITER, RestaurantId = mcdonalds.Id },
                   new User() { FirstName = "Dariusz", LastName = "Caban", Username = "dcaban", Password = "łyndołs", Rights = UserType.WAITER, RestaurantId = mcdonalds.Id }
               );

            context.SaveChanges();
            }

        }
    }

}
