using NotesApp.DAL;
using NotesApp.DAL.Repositories;
using NotesApp.DataModels;
using NotesApp.Services.Interfaces;
using SEDC.Notes.InerfaceModels.Enums;
using SEDC.Notes.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly IRepository<NoteDto> _noteRepository;

        public NoteService(IRepository<NoteDto> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public List<NoteModel> GetAll() 
        {
            var notes = _noteRepository.GetAll();

            var response = new List<NoteModel>();

            foreach (var note in notes)
            {
                var noteModel = new NoteModel
                {
                    Id = note.Id,
                    Color = note.Color,
                    Text = note.Text,
                    Tag = (TagType)note.Tag,
                    UserId = note.UserId
                };

                response.Add(noteModel);
            }

            return response;
        }

        public NoteModel GetById(int id) 
        {
            var note = _noteRepository.GetById(id);

            var noteModel = new NoteModel
            {
                Id = note.Id,
                Color = note.Color,
                Text = note.Text,
                Tag = (TagType)note.Tag,
                UserId = note.UserId
            };

            return noteModel;
        }

        public void Create(NoteModel model) 
        {
            var note = new NoteDto
            {
                Color = model.Color,
                Text = model.Text,
                Tag = (int)model.Tag,
                UserId = model.UserId
            };

            _noteRepository.Add(note);
        }

        public void Delete(int id) 
        {
            var note = _noteRepository.GetById(id);
            _noteRepository.Delete(note);
        }

    }
}
