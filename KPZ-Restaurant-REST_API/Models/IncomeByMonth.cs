using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_Restaurant_REST_API.Models
{
    public class IncomeByMonth
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public string Month { get; set; }
        [Column(TypeName = "decimal(9, 2)")]
        public decimal Income { get; set; }
    }
}