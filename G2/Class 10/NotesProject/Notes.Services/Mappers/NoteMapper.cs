using Notes.Contracts.DTOs;
using Notes.Domain.Entities;

namespace Notes.Services.Mappers
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto()
            {
                Id = note.Id,
                Name = note.Name,
                Description = note.Description
            };
        }

        public static Note ToNote(this NoteDto noteDto)
        {
            return new Note()
            {
                Name = noteDto.Name,
                Description = noteDto.Description
            };
        }
    }
}
