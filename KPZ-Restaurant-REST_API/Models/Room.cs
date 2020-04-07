using System.ComponentModel.DataAnnotations;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    }
}