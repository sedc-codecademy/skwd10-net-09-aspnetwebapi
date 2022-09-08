using SEDC.WebApi.Workshop.Notes.DataModels.Models;

namespace SEDC.WebApi.Workshop.Notes.DataAccess.Repositories
{
    public class NoteRepository : IRepository<Note>
    {
        public int Delete(Note entity)
        {
            var count = InMemoryDb.Notes.Count;
            InMemoryDb.Notes.Remove(entity);
            return count - InMemoryDb.Notes.Count;
        }

        public IEnumerable<Note> FilterBy(Func<Note, bool> filter)
        {
            return InMemoryDb.Notes.Where(filter);
        }

        public IEnumerable<Note> GetAll()
        {
            return InMemoryDb.Notes;
        }

        public Note GetById(int id)
        {
            return InMemoryDb.Notes.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Note entity)
        {
            var count = InMemoryDb.Notes.Count;
            InMemoryDb.Notes.Add(entity);
            return InMemoryDb.Notes.Count - count;
        }

        public int Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
