using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId {get; set;}
        
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }
        public string Name { get; set; }
    }
}