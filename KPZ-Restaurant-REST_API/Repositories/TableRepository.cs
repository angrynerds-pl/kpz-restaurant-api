using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class TableRepository: RestaurantGeneric<Table>, ITableRepository
    {
        RestaurantContext _context;

        public TableRepository(RestaurantContext context): base(context) 
        {
            _context = context
        }
    }
}
