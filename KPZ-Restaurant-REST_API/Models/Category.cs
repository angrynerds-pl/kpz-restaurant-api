using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_Restaurant_REST_API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        //[Column(TypeName ="NVARCHAR")]
        public string Name { get; set; }
    }
}