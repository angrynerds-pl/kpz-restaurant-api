using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class UsersRepository : RestaurantGeneric<User>, IUsersRepository
    {
        private RestaurantContext _context;
        public UsersRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllFiltered(Func<User, bool> filter)
        {
            return await _context.Users.Where(x => filter(x)).ToListAsync();
        }

        public async Task<bool> CheckIfPresent(User user)
        {
            return await _context.Set<User>().AnyAsync(u => u.Username == user.Username);
        }

        public async Task<List<User>> GetAllByRights(UserType rights)
        {    
            return await _context.Users.Where( x => x.Rights == rights ).ToListAsync(); 
        }
    }
}
