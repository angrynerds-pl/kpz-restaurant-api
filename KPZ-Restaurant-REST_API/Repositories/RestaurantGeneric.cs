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


        public T Create(T entity)
        {
           return _context.Set<T>().Add(entity).Entity;
        }

        public T DeleteById(int id)
        {
            T entities = _context.Set<T>().Find(id);
            return _context.Set<T>().Remove(entities).Entity;
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

        public T Update(T entity)
        {
            return _context.Update(entity).Entity;
        }
    }
}
