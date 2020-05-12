using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId {get; set;}
        
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        //[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TableId { get; set; }
        [ForeignKey("TableId")]
        public virtual Table Table { get; set; }
        public int NumberOfSeats { get; set; }
        //[Column(TypeName="NVARCHAR")]
        public string CustomerName { get; set; }
        public DateTime? DeletedAt { get; set; }


    }
}
