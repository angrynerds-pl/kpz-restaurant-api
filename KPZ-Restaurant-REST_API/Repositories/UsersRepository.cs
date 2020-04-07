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
    }
}
