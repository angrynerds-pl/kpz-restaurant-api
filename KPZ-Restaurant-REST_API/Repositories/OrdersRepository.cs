using KPZ_Restaurant_REST_API.Models;
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

        public bool OrderCorrect(Order order)
        {
            return _context.Set<Table>().Any(t => t.Id == order.TableId)
                && _context.Set<User>().Any(w => w.Id == order.Id)
                && _context.Set<Restaurant>().Any(r => r.Id == order.RestaurantId);
        }
    }
}
