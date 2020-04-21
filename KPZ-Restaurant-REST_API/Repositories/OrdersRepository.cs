using KPZ_Restaurant_REST_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class OrdersRepository : RestaurantGeneric<Order>, IOrdersRepository
    {
        private RestaurantContext _context;

        public OrdersRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> OrderCorrect(Order order)
        {
            return await _context.Set<Table>().AnyAsync(t => t.Id == order.TableId)
                && await _context.Set<User>().AnyAsync(w => w.Id == order.WaiterId)
                && await _context.Set<Restaurant>().AnyAsync(r => r.Id == order.RestaurantId);
        }
    }
}
