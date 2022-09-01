using Notes.Domain.Entities;
using Notes.Storage.Database;

namespace Notes.Storage.Repositories
{
    public abstract class RepositoryBase<T> where T : BaseEntity
    {
        protected readonly INotesDbContext _notesDbContext;

        public RepositoryBase(INotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }

        protected IQueryable<T> GetById(int id)
        {
            return GetAll().Where(x => x.Id == id);
        }

        protected IQueryable<T> GetAll()
        {
            return _notesDbContext.Set<T>();
        }

        protected void InsertEntity(T item)
        {
            _notesDbContext.Set<T>().Add(item);
        }

        protected void UpdateEntity(T item)
        {
            _notesDbContext.Set<T>().Update(item);
        }

        protected void DeleteEntity(int id)
        {
            _notesDbContext.Set<T>().Remove(GetById(id).SingleOrDefault());
        }
    }
}
