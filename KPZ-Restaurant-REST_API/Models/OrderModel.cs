using System.Collections.Generic;

namespace KPZ_Restaurant_REST_API.Models
{
    public class OrderModel
    {
        public int RoomNumber { get; set; }
        public int TableNumber { get; set; }
        public string WaiterUsername { get; set; }
        public string Note { get; set; }
        public List<Product> OrderedProducts { get; set; }  
    }
}