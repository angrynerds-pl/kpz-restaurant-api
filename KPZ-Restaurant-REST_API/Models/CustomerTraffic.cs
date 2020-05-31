using System;

namespace KPZ_Restaurant_REST_API.Models
{
    public class CustomerTraffic
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Quantity { get; set; }
    }
}