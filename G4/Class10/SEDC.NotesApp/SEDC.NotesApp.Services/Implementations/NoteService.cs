using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;

namespace SEDC.NotesApp.Services.Implementations
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> _noteRepository;
        private readonly IUserRepository _userRepository;

        public NoteService(IRepository<Note> noteRepository, IUserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public void AddNote(AddNoteDto addNoteDto)
        {
            //1. validation
            User userDb = _userRepository.GetById(addNoteDto.UserId);
            if(userDb == null)
            {
                throw new NoteDataException($"User with id {addNoteDto.UserId} does not exist");
            }

            if (string.IsNullOrEmpty(addNoteDto.Text))
            {
                throw new NoteDataException("Text is required field");
            }
            //Text is not null or empty
            if(addNoteDto.Text.Length > 100)
            {
                throw new NoteDataException("Text can not contain more than 100 characters");
            }

            //2. map to domain model
            Note newNote = addNoteDto.ToNote();
            newNote.User = userDb;

            //3. add to db
            _noteRepository.Add(newNote);
        }

        public void DeleteNote(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if(noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found");
            }

            _noteRepository.Delete(noteDb);
        }

        public List<NoteDto> GetAllNotes()
        {
            var notesDb = _noteRepository.GetAll();
            return notesDb.Select(x => x.ToNoteDto()).ToList();
        }

        public NoteDto GetById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {id} was not found!");
            }
            NoteDto noteDto = noteDb.ToNoteDto();
            return noteDto;
        }

        public void UpdateNote(UpdateNoteDto note)
        {
            //1.validation
            Note noteDb = _noteRepository.GetById(note.Id);
            if(noteDb == null)
            {
                throw new NoteNotFoundException($"Note with id {note.Id} was not found!");
            }

            User userDb = _userRepository.GetById(note.UserId);
            if (userDb == null)
            {
                throw new NoteDataException($"User with id {note.UserId} does not exist");
            }

            if (string.IsNullOrEmpty(note.Text))
            {
                throw new NoteDataException("Text is required field");
            }
            //Text is not null or empty
            if (note.Text.Length > 100)
            {
                throw new NoteDataException("Text can not contain more than 100 characters");
            }

            //2.update
            //We must update the object that we read from db
            noteDb.Text = note.Text;
            noteDb.Priority = note.Priority;
            noteDb.Tag = note.Tag;
            noteDb.UserId = note.UserId;
            noteDb.User = userDb;

            _noteRepository.Update(noteDb);
        }
    }
}
