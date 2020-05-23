using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPZ_Restaurant_REST_API.Models
{
    public class IncomeByMonth
    {
        public string Month { get; set; }
        public decimal Income { get; set; }
    }
}