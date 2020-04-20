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
            
            if (!context.Restaurants.Any())
            {
                context.Restaurants.AddRange(
                    mcdonalds
                );
                context.SaveChanges();
            }


            if (!context.Users.Any())
            {
                //Seed data
                Console.WriteLine("Adding data to User table");
                context.Users.AddRange(
                    new User() { FirstName = "Jakub", LastName = "Faldasz", Username = "jfaldasz", Password = "passw0rd", Rights = UserType.WAITER, RestaurantId = mcdonalds.Id },
                    new User() { FirstName = "Jan", LastName = "Kowalski", Username = "jkowalski", Password = "k0valsk1", Rights = UserType.WAITER, RestaurantId = mcdonalds.Id }
                );
                context.SaveChanges();
            }
            else
            {

            }

        }
    }

}
