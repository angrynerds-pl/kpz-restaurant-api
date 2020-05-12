using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId {get; set;}
        
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public virtual List<Table> Tables { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}