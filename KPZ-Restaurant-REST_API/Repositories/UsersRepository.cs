using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class UsersRepository : RestaurantGeneric<User>, IUsersRepository
    {
        private RestaurantContext _context;
        public UsersRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }

        public bool CheckIfPresent(User user)
        {
            return _context.Set<User>().Any(u => u.Username == user.Username);
        }

        public List<User> GetAllByRights(int rights)
        {
            var result = _context.Users.Where(s => s.Rights == 1).ToList();
            return result;
        }
    }
}
