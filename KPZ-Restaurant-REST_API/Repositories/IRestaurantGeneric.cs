using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IRestaurantGeneric <T> where T: class
    {
        Task<List<T>> GetAll();
        IQueryable<T> Get();
        Task<T> GetById(int id);
        void DeleteById(int id);
        void Update(T entity);
        Task<ICollection<T>> GetWhere(Expression<Func<T, bool>> predicate);
        Task<T> FindOne(Expression<Func<T,bool>> predicate);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> elements);
        Task SaveAsync();
    }
}
