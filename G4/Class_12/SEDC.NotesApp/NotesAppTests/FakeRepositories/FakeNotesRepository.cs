using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppTests.FakeRepositories
{
    public class FakeNotesRepository : IRepository<Note>
    {
        private List<Note> notes;
        public FakeNotesRepository()
        {
            notes = new List<Note>()
                {
                    new Note()
                    {
                        Id = 1,
                        UserId = 1,
                        Priority = SEDC.NotesApp.Domain.Enums.Priority.High,
                        Tag = SEDC.NotesApp.Domain.Enums.Tag.Health,
                        Text = "Do something"
                    },
                    new Note()
                    {
                        Id = 2,
                        UserId = 1,
                        Priority = SEDC.NotesApp.Domain.Enums.Priority.Medium,
                        Tag = SEDC.NotesApp.Domain.Enums.Tag.SocialLife,
                        Text = "Do something else!"
                    }
                };
        }
        public void Add(Note entity)
        {
            notes.Add(entity);
        }

        public void Delete(Note entity)
        {
            notes.Remove(entity);
        }

        public List<Note> GetAll()
        {
            return notes;
        }

        public Note GetById(int id)
        {
            return notes.SingleOrDefault(note => note.Id == id);
        }

        public void Update(Note entity)
        {
            notes[notes.IndexOf(entity)] = entity;
        }
    }
}
