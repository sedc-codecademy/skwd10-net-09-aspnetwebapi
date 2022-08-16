using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL.Repositories
{
    public class NoteEntityRepository : IRepository<NoteDto>
    {
        public void Add(NoteDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(NoteDto entity)
        {
            throw new NotImplementedException();
        }

        public List<NoteDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public NoteDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(NoteDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
