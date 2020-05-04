using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId {get; set;}
        
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public int TableId { get; set; }
        [ForeignKey("TableId")]
        public virtual Table Table { get; set; }
        public int WaiterId { get; set; }
        [ForeignKey("WaiterId")]
        public virtual User Waiter { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderedProducts> OrderedProducts {get; set;}

    }
}
