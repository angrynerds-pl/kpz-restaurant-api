using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public interface IRestaurantGeneric <T> where T: class
    {
        List<T> GetAll();
        IQueryable<T> Get();
        T GetById(int id);
        T DeleteById(int id);
        T Update(T entity);
        T Create(T entity);
        void Save();
    }
}
