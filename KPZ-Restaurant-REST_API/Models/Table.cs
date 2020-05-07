using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int Seats { get; set; }
        public string Status { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
