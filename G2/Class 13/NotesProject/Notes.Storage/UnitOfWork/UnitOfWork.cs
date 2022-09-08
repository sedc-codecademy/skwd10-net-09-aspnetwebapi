using Notes.Domain.UnitOfWork;
using Notes.Storage.Database;

namespace Notes.Storage.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly INotesDbContext _notesDbContext;

        public UnitOfWork(INotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            if (_notesDbContext.ChangeTracker.HasChanges())
            {
                return await _notesDbContext.SaveChangesAsync();
            }

            return 0;
        }
    }
}
