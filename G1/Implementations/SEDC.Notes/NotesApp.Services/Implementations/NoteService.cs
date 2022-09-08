using AutoMapper;
using NotesApp.DAL;
using NotesApp.DAL.Repositories;
using NotesApp.DataModels;
using NotesApp.Exceptions;
using NotesApp.InerfaceModels.Models;
using NotesApp.Mapper;
using NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Implementations
{
    //AutoMapper
    public class NoteService : INoteService
    {
        private readonly IRepository<NoteDto> _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(IRepository<NoteDto> noteRepository, 
                           IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
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

        public NoteModel GetById(int id, int userId) 
        {
            return NoteMapper.ToNoteModel(_noteRepository.GetById(id, userId));
        }

        public void Create(NoteModel model) 
        {
            if (string.IsNullOrEmpty(model.Text)) 
            {
                throw new NoteException(model.Id, model.UserId, "Text field is required");
            }

            //_noteRepository.Add(NoteMapper.ToNoteDto(model));
            _noteRepository.Add(_mapper.Map<NoteDto>(model));
        }

        public void Delete(int id) 
        {
            if (id == null) 
            {
                throw new NoteException(id, null, "Id is required");
            }

            _noteRepository.Delete(_noteRepository.GetById(id));
        }
    }
}
