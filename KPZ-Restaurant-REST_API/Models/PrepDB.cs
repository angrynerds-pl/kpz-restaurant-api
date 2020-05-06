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
                   new Room() { RestaurantId = mcdonalds.Id, Restaurant = context.Restaurants.FirstOrDefault(), Name = "Sala 1", Rows = 3, Columns = 3 }
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
                   },
                   new Table()
                   {
                       Number = 2,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 1,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 3,
                       Seats = 6,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 2,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 4,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 3,
                       Y = 0
                   }
               );
                context.SaveChanges();


            }

            //Add categories
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category()
                    {
                        RestaurantId = mcdonalds.Id,
                        Name = "BURGERS"
                    },
                     new Category()
                     {
                         RestaurantId = mcdonalds.Id,
                         Name = "WRAPS"
                     },
                     new Category()
                     {
                         RestaurantId = mcdonalds.Id,
                         Name = "FRIES"
                     }
               );
                context.SaveChanges();

            }

            //Add product
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Big Mac", Price = 12.99M, CategoryId = context.Categories.FirstOrDefault(c=>c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "McRoyal", Price = 13.99M, CategoryId = context.Categories.FirstOrDefault(c=>c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Hamburger", Price = 4.99M, CategoryId = context.Categories.FirstOrDefault(c=>c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Cheeseburger", Price = 5.99M, CategoryId = context.Categories.FirstOrDefault(c=>c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "McWrap", Price = 15.99M, CategoryId = context.Categories.FirstOrDefault(c=>c.Name == "WRAPS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Medium Fries", Price = 4.99M, CategoryId = context.Categories.FirstOrDefault(c=>c.Name == "FRIES").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Small Fries", Price = 3.99M, CategoryId = context.Categories.FirstOrDefault(c=>c.Name == "FRIES").Id }
               );

                context.SaveChanges();
            }

            //Add users
            if (!context.Users.Any())
            {
                //Seed data
                Console.WriteLine("Adding data to User table");
                context.Users.AddRange(
                   new User() { FirstName = "Janusz", LastName = "Biernat", Username = "jbiernat", Password = "architektura", Rights = UserType.MANAGER, RestaurantId = mcdonalds.Id },
                   new User() { FirstName = "Dariusz", LastName = "Caban", Username = "dcaban", Password = "łyndołs", Rights = UserType.WAITER, RestaurantId = mcdonalds.Id },
                   new User() { FirstName = "Jacek", LastName = "Mazurkiewicz", Username = "jmazur", Password = "mikrokontroler", Rights = UserType.COOK, RestaurantId = mcdonalds.Id },
                   new User() { FirstName = "Piotr", LastName = "Patronik", Username = "pepe", Password = "przepis", Rights = UserType.HEAD_WAITER, RestaurantId = mcdonalds.Id }
               );

                context.SaveChanges();
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(1).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.WAITER).Id,
                        OrderDate = DateTime.Now,
                        // Comment = "na szybko"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(2).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.WAITER).Id,
                        OrderDate = DateTime.Now,
                        // Comment = "bez pośpiechu"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(3).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.WAITER).Id,
                        OrderDate = DateTime.Now,
                        // Comment = "weganie, tfu"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(4).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now,
                        // Comment = "przynieść krzesło dla bachora"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(1).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now,
                        // Comment = "xD"
                    }
                );

                context.SaveChanges();
            }

            // if (!context.OrderedProducts.Any())
            // {
            //     context.Set<OrderedProducts>().AddRange(
            //         new OrderedProducts()
            //         {
            //             OrderId = 1,
            //             ProductId = context.Products.FirstOrDefault(p => p.Name == "Hamburger").Id,
            //             Status = "READY"
            //         },
            //         new OrderedProducts()
            //         {
            //             OrderId = 1,
            //             ProductId = context.Products.FirstOrDefault(p => p.Name == "Medium Fries").Id,
            //             Status = "IN_PROGRESS"
            //         },
            //         new OrderedProducts()
            //         {
            //             OrderId = 1,
            //             ProductId = context.Products.FirstOrDefault(p => p.Name == "McWrap").Id,
            //             Status = "IN_PROGRESS"
            //         },
            //         new OrderedProducts()
            //         {
            //             OrderId = 1,
            //             ProductId = context.Products.FirstOrDefault(p => p.Name == "BigMac").Id,
            //             Status = "IN_PROGRESS"
            //         },
            //         new OrderedProducts()
            //         {
            //             OrderId = 3,
            //             ProductId = context.Products.FirstOrDefault(p => p.Name == "McRoyal").Id,
            //             Status = "IN_PROGRESS"
            //         },
            //         new OrderedProducts()
            //         {
            //             OrderId = 4,
            //             ProductId = context.Products.FirstOrDefault(p => p.Name == "Hamburger").Id,
            //             Status = "READY"
            //         },
            //         new OrderedProducts()
            //         {
            //             OrderId = 4,
            //             ProductId = context.Products.FirstOrDefault(p => p.Name == "Small Fries").Id,
            //             Status = "READY"
            //         }
            //     );
            //     context.SaveChanges();
            // }


        }
    }

}
