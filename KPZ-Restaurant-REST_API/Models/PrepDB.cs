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
        public static async Task PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedData(serviceScope.ServiceProvider.GetService<RestaurantContext>());
            }

        }
        public static async Task SeedData(RestaurantContext context)
        {
            System.Console.WriteLine("Appling Migrations...");

            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();


            var mcdonalds = new Restaurant() { Name = "McDonalds" };
            
            if (!context.Restaurants.Any())
            {
                context.Restaurants.AddRange(
                    mcdonalds
                );
                await context.SaveChangesAsync();
            }


            if (!context.Users.Any())
            {
                //Seed data
                Console.WriteLine("Adding data to User table");
                context.Users.AddRange(
                    new User() { FirstName = "Janusz", LastName = "Biernat", Username = "jbiernat", Password = "architektura", Rights = UserType.WAITER, RestaurantId = mcdonalds.Id },
                    new User() { FirstName = "Dariusz", LastName = "Caban", Username = "dcaban", Password = "łyndołs", Rights = UserType.WAITER, RestaurantId = mcdonalds.Id }
                );
                await context.SaveChangesAsync();
            }
            else
            {

            }

        }
    }

}
