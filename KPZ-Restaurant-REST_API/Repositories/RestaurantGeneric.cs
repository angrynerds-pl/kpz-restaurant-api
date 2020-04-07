using KPZ_Restaurant_REST_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPZ_Restaurant_REST_API.Repositories
{
    public class RestaurantGeneric<T> : IRestaurantGeneric<T> where T : class
    {

        private readonly RestaurantContext _context;

        public RestaurantGeneric(RestaurantContext context)
        {
            _context = context;
        }


        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void DeleteById(int id)
        {
            T entities = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entities);
        }

        public IQueryable<T> Get()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
