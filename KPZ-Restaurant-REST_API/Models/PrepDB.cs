﻿using Microsoft.AspNetCore.Builder;
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
                   new Room() { RestaurantId = mcdonalds.Id, Restaurant = context.Restaurants.FirstOrDefault(), Name = "Floor 1", Rows = 5, Columns = 4 },
                   new Room() { RestaurantId = mcdonalds.Id, Restaurant = context.Restaurants.FirstOrDefault(), Name = "Floor 2", Rows = 4, Columns = 3 }
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
                       Y = 1
                   },
                   new Table()
                   {
                       Number = 2,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 0,
                       Y = 2
                   },
                   new Table()
                   {
                       Number = 3,
                       Seats = 6,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 1,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 4,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 2,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 5,
                       Seats = 6,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 1,
                       Y = 3
                   },
                   new Table()
                   {
                       Number = 6,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 2,
                       Y = 3
                   },
                   new Table()
                   {
                       Number = 7,
                       Seats = 3,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 3,
                       Y = 1
                   },
                   new Table()
                   {
                       Number = 8,
                       Seats = 2,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 3,
                       Y = 2
                   },
                   new Table()
                   {
                       Number = 9,
                       Seats = 3,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 4,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 10,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.FirstOrDefault().Id,
                       X = 4,
                       Y = 3
                   },
                   new Table()
                   {
                       Number = 11,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.Where(r => r.Id == 2).FirstOrDefault().Id,
                       X = 0,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 12,
                       Seats = 3,
                       Status = "FREE",
                       RoomId = context.Rooms.Where(r => r.Id == 2).FirstOrDefault().Id,
                       X = 1,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 13,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.Where(r => r.Id == 2).FirstOrDefault().Id,
                       X = 2,
                       Y = 0
                   },
                   new Table()
                   {
                       Number = 14,
                       Seats = 4,
                       Status = "FREE",
                       RoomId = context.Rooms.Where(r => r.Id == 2).FirstOrDefault().Id,
                       X = 2,
                       Y = 1
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
                        Name = "BURGERS",
                        ImagePath = "burgers.png"
                    },
                     new Category()
                     {
                         RestaurantId = mcdonalds.Id,
                         Name = "WRAPS",
                         ImagePath = "wraps.png"
                     },
                     new Category()
                     {
                         RestaurantId = mcdonalds.Id,
                         Name = "FRIES",
                         ImagePath = "fries.png"
                     },
                     new Category()
                     {
                         RestaurantId = mcdonalds.Id,
                         Name = "SALADS",
                         ImagePath = "salads.png"
                     }
               );
                context.SaveChanges();

            }

            //Add product
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Big Mac", Price = 12.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "McRoyal", Price = 13.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Hamburger", Price = 4.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Cheeseburger", Price = 5.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "BURGERS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "McWrap", Price = 15.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "WRAPS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Medium Fries", Price = 4.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "FRIES").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Small Fries", Price = 3.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "FRIES").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Large Fries", Price = 7.00M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "FRIES").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Cesar Salad", Price = 7.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "SALADS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Small Salad", Price = 6.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "SALADS").Id },
                  new Product() { RestaurantId = mcdonalds.Id, Name = "Large Salad", Price = 5.99M, CategoryId = context.Categories.FirstOrDefault(c => c.Name == "SALADS").Id }
               );

                context.SaveChanges();
            }

            //Add users
            if (!context.Users.Any())
            {
                //Seed data
                Console.WriteLine("Adding data to User table");
                context.Users.AddRange(
                   new User() { FirstName = "Jan", LastName = "Kowalski", Username = "jkowalski", Password = PasswordHasher.HashPassword("kowalski"), Rights = UserType.MANAGER, RestaurantId = mcdonalds.Id },
                   new User() { FirstName = "Andrzej", LastName = "Nowak", Username = "anowak", Password = PasswordHasher.HashPassword("nowak"), Rights = UserType.WAITER, RestaurantId = mcdonalds.Id },
                   new User() { FirstName = "Jacek", LastName = "Kulczyk", Username = "jkulczyk", Password = PasswordHasher.HashPassword("kulczyk"), Rights = UserType.COOK, RestaurantId = mcdonalds.Id },
                   new User() { FirstName = "Dariusz", LastName = "Mariusz", Username = "dmariusz", Password = PasswordHasher.HashPassword("mariusz"), Rights = UserType.HEAD_WAITER, RestaurantId = mcdonalds.Id }
               );

                context.SaveChanges();
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(2).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.WAITER).Id,
                        OrderDate = DateTime.Now,
                        Note = "na szybko",
                        Status = "IN_PROGRESS"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(2).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.WAITER).Id,
                        OrderDate = DateTime.Now.AddDays(3),
                        Note = "bez pośpiechu",
                        Status = "PENDING"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(3).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.WAITER).Id,
                        OrderDate = DateTime.Now,
                        Note = "weganie, tfu",
                        Status = "PAID"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(4).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now.AddMonths(-1),
                        Note = "przynieść krzesło dla bachora",
                        Status = "PAID"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(2).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now.AddMonths(-3),
                        Note = "xD",
                        Status = "PAID"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(4).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now.AddDays(-45),
                        Note = "xD",
                        Status = "PAID"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(5).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now.AddDays(-2),
                        Note = "xD",
                        Status = "PAID"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(3).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now.AddDays(-4),
                        Note = "xD",
                        Status = "PAID"
                    },
                    new Order()
                    {
                        RestaurantId = mcdonalds.Id,
                        TableId = context.Tables.Find(2).Id,
                        WaiterId = context.Users.FirstOrDefault(w => w.Rights == UserType.HEAD_WAITER).Id,
                        OrderDate = DateTime.Now.AddMonths(-2),
                        Note = "xD",
                        Status = "PAID"
                    }
                );

                context.SaveChanges();
            }

            if (!context.OrderedProducts.Any())
            {
                context.OrderedProducts.AddRange(
                      new OrderedProducts()
                      {
                          OrderId = 1,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Hamburger").Id,
                          Status = "READY"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 1,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Medium Fries").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 1,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "McWrap").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 1,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Big Mac").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 3,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "McRoyal").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 4,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Hamburger").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 4,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Small Fries").Id,
                          Status = "READY"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 2,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Small Fries").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 2,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Hamburger").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 4,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Cheeseburger").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 4,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Cesar Salad").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 5,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Big Mac").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 5,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Large Fries").Id,
                          Status = "PAID"
                      },
                      new OrderedProducts()
                      {
                          OrderId = 5,
                          ProductId = context.Products.FirstOrDefault(p => p.Name == "Small Salad").Id,
                          Status = "PAID"
                      }
                  );
                context.SaveChanges();
            }


        }
    }

}
