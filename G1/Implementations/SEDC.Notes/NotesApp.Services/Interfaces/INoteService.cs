﻿using NotesApp.InerfaceModels.Models;
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
        List<NoteModel> GetAll(int userId);
        NoteModel GetById(int id, int userId);
        void Create(NoteModel model);
        void Delete(int id);
    }
}
