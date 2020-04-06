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
        public int TableId { get; set; }
        [ForeignKey("TableId")]
        public virtual Table Table { get; set; }
        public DateTime OrderDate { get; set; }


    }
}
