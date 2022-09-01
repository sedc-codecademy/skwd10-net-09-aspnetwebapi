using MovieManager.Application.Repositories;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Infrastructure.Repositories
{
    public class BaseEFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        public BaseEFRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return entity;
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public T? GetById(int id)
        {
            return Query().FirstOrDefault(x => x.Id == id);
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
