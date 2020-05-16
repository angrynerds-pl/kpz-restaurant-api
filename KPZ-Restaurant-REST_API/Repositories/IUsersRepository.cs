using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IUsersRepository : IRestaurantGeneric<User>
    {
        Task<bool> CheckIfPresent(User user);

        Task<ICollection<User>> GetAllFiltered(Expression<Func<User, bool>> predicate);
        Task<User> GetUserById(int id, int restaurantId);
        Task<ICollection<User>> GetAllByRights( UserType rights, int restaurantId);
        Task<User> GetByUsername(string username);
        Task<ICollection<User>> GetAllUsers(int restaurantId);
        Task<User> DeleteUserById(int id, int restaurantId);
    }
}
