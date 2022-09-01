using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Domain.Repositories;
using Notes.Storage.Database;

namespace Notes.Storage.Repositories
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(INotesDbContext notesDbContext) : base(notesDbContext)
        {

        }

        public async Task DeleteNoteAsync(int id)
        {
            DeleteEntity(id);
        }

        public async Task<IReadOnlyList<Note>> GetAllNotesAsync()
        {
            IQueryable<Note> noteQuery = GetAll();

            return await noteQuery.ToArrayAsync();
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            return await GetById(id).SingleOrDefaultAsync();
        }

        public async Task InsertNoteAsync(Note newNote)
        {
            InsertEntity(newNote);
        }

        public async Task UpdateNoteAsync(Note existingNote)
        {
            UpdateEntity(existingNote);
        }
    }
}
