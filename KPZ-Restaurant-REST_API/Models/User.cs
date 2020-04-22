using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Models
{

    public enum UserType { MANAGER, COOK, WAITER, HEAD_WAITER}
    public class User
    {

        [Key]
        public int Id { get; set; }
        public int RestaurantId {get; set;}
        
        [ForeignKey("RestaurantId")]
        public virtual Restaurant Restaurant { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Rights { get; set; }
    }
}
