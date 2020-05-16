using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class UsersRepository : RestaurantGeneric<User>, IUsersRepository
    {
        private RestaurantContext _context;
        public UsersRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllFiltered(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Where(predicate).Where(u => u.DeletedAt == null).ToListAsync();
        }

        public async Task<bool> CheckIfPresent(User user)
        {
            return await _context.Set<User>().AnyAsync(u => u.Username == user.Username && u.DeletedAt == null);
        }

        public async Task<ICollection<User>> GetAllByRights(UserType rights, int restaurantId)
        {    
            return await _context.Users.Where( x => x.RestaurantId == restaurantId && x.Rights == rights && x.DeletedAt == null ).ToListAsync(); 
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.Where(u => u.Username == username && u.DeletedAt == null).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserById(int id, int restaurantId)
        {
            return await _context.Users.Where(u => u.Id == id && u.RestaurantId == restaurantId && u.DeletedAt == null).FirstOrDefaultAsync();
        }

        public async Task<ICollection<User>> GetAllUsers(int restaurantId)
        {
            return await _context.Users.Where(u => u.RestaurantId == restaurantId && u.DeletedAt == null).ToListAsync();
        }

        public async Task<User> DeleteUserById(int id, int restaurantId)
        {
            var userToDelete = await _context.Users.Where(u => u.Id == id && u.RestaurantId == restaurantId && u.DeletedAt == null).FirstOrDefaultAsync();
            if(userToDelete != null)
            {
                userToDelete.DeletedAt = DateTime.Now;
                _context.Users.Update(userToDelete);
                return userToDelete;
            } 
            else
                return null;
        }
    }
}
