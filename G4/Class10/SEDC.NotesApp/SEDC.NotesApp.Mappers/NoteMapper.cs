using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Dtos;

namespace SEDC.NotesApp.Mappers
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(this Note note)
        {
            return new NoteDto
            {
                Tag = note.Tag,
                Priority = note.Priority,
                Text = note.Text,
                //UserFullName = $"{note.User.FirstName} {note.User.LastName}",
            };
        }

        public static Note ToNote(this AddNoteDto addNoteDto)
        {
            return new Note()
            {
                Text = addNoteDto.Text,
                Priority = addNoteDto.Priority,
                Tag = addNoteDto.Tag,
                UserId = addNoteDto.UserId, //FK
            };
        }

        public static Note ToNote(this UpdateNoteDto updateNoteDto, Note noteDb)
        {
            noteDb.Text = updateNoteDto.Text;
            //.....
            return noteDb;
        }
    }
}
