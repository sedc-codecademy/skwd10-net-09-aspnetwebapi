﻿using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL.Repositories
{
    public class NoteStaticDbRepoistory : IRepository<NoteDto>
    {
        public void Add(NoteDto entity)
        {
            StaticDB.Notes.Add(entity);
        }

        public void Delete(NoteDto entity)
        {
            StaticDB.Notes.Remove(entity);
        }

        public List<NoteDto> GetAll()
        {
            return StaticDB.Notes;
        }

        public NoteDto GetById(int id)
        {
            var result = StaticDB.Notes.FirstOrDefault(note => note.Id == id);
            return result;
        }

        public void Update(NoteDto entity)
        {
            var result = StaticDB.Notes.FirstOrDefault(note => note.Id == entity.Id);
            result.Text = entity.Text;
            result.Color = entity.Color;
            result.Tag = entity.Tag;
        }
    }
}
