using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KPZ_Restaurant_REST_API.Models;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class RestaurantRepository : RestaurantGeneric<Restaurant>, IRestaurantRepository
    {
        private RestaurantContext _context;

        public RestaurantRepository(RestaurantContext context) : base(context)
        {
            _context = context;
        }
    }
}
