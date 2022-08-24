using NotesApp.DAL;
using NotesApp.DAL.Repositories;
using NotesApp.DataModels;
using NotesApp.Mapper;
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
            return _noteRepository.GetAll()
                                  .Select(note => NoteMapper.ToNoteModel(note))
                                  .ToList();
        }

        public List<NoteModel> GetAll(int userId)
        {
            return _noteRepository.GetAll()
                                  .Where(note => note.UserId == userId)
                                  .Select(note => NoteMapper.ToNoteModel(note))
                                  .ToList();
        }

        public NoteModel GetById(int id) 
        {
            return NoteMapper.ToNoteModel(_noteRepository.GetById(id));
        }

        public void Create(NoteModel model) 
        {
            _noteRepository.Add(NoteMapper.ToNoteDto(model));
        }

        public void Delete(int id) 
        {
            _noteRepository.Delete(_noteRepository.GetById(id));
        }
    }
}
