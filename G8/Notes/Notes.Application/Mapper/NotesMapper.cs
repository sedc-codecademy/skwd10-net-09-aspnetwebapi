using Notes.Application.Models;
using Notes.Domain.Models;

namespace Notes.Application.Mapper
{
    public static class NotesMapper
    {
        public static NoteModel ToModel(this Note note)
        {
            return new NoteModel
            {
                Id = note.Id,
                Color = note.Color,
                Tag = note.Tag,
                Text = note.Text,
                UserName = note.User.Username
            };
        } 

    }
}
