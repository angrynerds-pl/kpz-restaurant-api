using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId { get; set; }

        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}