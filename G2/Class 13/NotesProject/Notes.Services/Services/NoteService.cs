using Notes.Contracts.DTOs;
using Notes.Contracts.Services;
using Notes.Domain.Entities;
using Notes.Domain.Repositories;
using Notes.Domain.UnitOfWork;
using Notes.Services.Mappers;

namespace Notes.Services.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NoteService(INoteRepository noteRepository, IUnitOfWork unitOfWork)
        {
            _noteRepository = noteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateNoteAsync(NoteDto newNote)
        {
            Note note = newNote.ToNote();
            await _noteRepository.UpdateNoteAsync(note);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            await _noteRepository.DeleteNoteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<NoteDto>> GetAllNotesAsync()
        {
            IReadOnlyList<Note> notes = await _noteRepository.GetAllNotesAsync();

            if (notes.Count > 0)
            {
                return null;
            }

            return notes.Select(x => x.ToNoteDto()).ToArray();
        }

        public async Task<NoteDto> GetNoteAsync(int id)
        {
            Note note = await _noteRepository.GetNoteAsync(id);

            if (note == null)
            {
                return null;
            }

            return note.ToNoteDto();
        }

        public async Task UpdateNoteAsync(NoteDto existingNote)
        {
            Note note = await _noteRepository.GetNoteAsync(existingNote.Id);

            note.ToNoteDto();
            _noteRepository.UpdateNoteAsync(note);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
