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

        public async Task<IEnumerable<Order>> GetAllOrders(int restaurantId)
        {
            return await _context.Set<Order>().Where(o => o.RestaurantId == restaurantId).Include(o => o.OrderedProducts).ThenInclude(p => p.Product).ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId, int restaurantId)
        {
            return await _context.Orders.Where(o => o.RestaurantId == restaurantId && o.Id == orderId).Include(o => o.OrderedProducts).ThenInclude(p => p.Product).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDate(DateTime serachedDate, int restaurantId)
        {
            return await _context.Orders.Where(o => o.RestaurantId == restaurantId && o.OrderDate.Date == serachedDate.Date).Include(o => o.OrderedProducts).ThenInclude(p => p.Product).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersForTable(int tableId, int restaurantId)
        {
            return await _context.Orders.Where(o => o.TableId == tableId && o.Status == "IN_PROGRESS" && o.RestaurantId == restaurantId).Include(o => o.OrderedProducts).ThenInclude(p => p.Product).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersInProgress(int restaurantId)
        {
            return await _context.Orders.Where(o => o.Status == "IN_PROGRESS" && o.RestaurantId == restaurantId).Include(o => o.OrderedProducts).ThenInclude(p => p.Product).ToListAsync();
        }

        public async Task<bool> OrderCorrect(Order order)
        {
            return await _context.Set<Table>().AnyAsync(t => t.Id == order.TableId)
                && await _context.Set<User>().AnyAsync(w => w.Id == order.WaiterId)
                && await _context.Set<Restaurant>().AnyAsync(r => r.Id == order.RestaurantId)
                && !(await _context.Set<Order>().AnyAsync(o =>
                    o.Id != order.Id
                    && o.TableId == order.TableId
                    && o.OrderDate.Date == order.OrderDate.Date
                    && o.OrderDate.Hour == order.OrderDate.Hour
                    && o.Status != "PAID"));
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var entity = await _context.Orders.FindAsync(order.Id);
            if (entity != null)
            {
                entity.TableId = order.TableId;
                entity.WaiterId = order.WaiterId;
                entity.Note = order.Note;
                entity.OrderDate = order.OrderDate;
                entity.Status = order.Status;
                _context.Orders.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            else
                return null;
        }
    }
}
