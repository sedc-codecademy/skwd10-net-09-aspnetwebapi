using SEDC.Notes.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Interfaces
{
    public interface INoteService
    {
        List<NoteModel> GetAll();
        NoteModel GetById(int id);
        void Create(NoteModel model);
        void Delete(int id);
    }
}
