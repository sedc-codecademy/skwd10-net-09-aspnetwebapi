using Notes.Domain.Models;

namespace Notes.Application.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        T? GetById(int id);

        IQueryable<T> GetAll();

        T Create(T entity);

        T Update(T entity);

        T Delete(T entity);
    }
}
