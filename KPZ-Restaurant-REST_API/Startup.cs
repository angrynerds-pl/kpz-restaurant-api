using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using KPZ_Restaurant_REST_API.Services;
using KPZ_Restaurant_REST_API.Repositories;

namespace KPZ_Restaurant_REST_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var user = Configuration["DBUser"] ?? "SA";//"resLogin";
            var password = Configuration["DBPassword"] ?? "kpz-restaurant-passw0rd";//"kpz-passw0rd";
            var database = Configuration["Database"] ?? "kpz_restaurant";


            services.AddDbContext<RestaurantContext>(options =>
               options.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password}")
            );

            //TODO: Create UnitOfWork for repositories
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IProductsInOrderRepository, ProductsInOrderRepository>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepDB.PrepPopulation(app);
        }
    }
}
