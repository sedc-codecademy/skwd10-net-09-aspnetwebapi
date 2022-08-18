using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using SEDC.WebApi.Workshop.Notes.ServiceModels.Enums;
using SEDC.WebApi.Workshop.Notes.ServiceModels.NotesModels;

namespace SEDC.WebApi.Workshop.Notes.Common.MappingHelpers
{
    public static class Mapper
    {
        public static NoteDto Map(this Note note)
        {
            return new NoteDto
            {
                Color = note.Color,
                Id = note.Id,
                Tag = (TagType)note.Tag,
                Text = note.Text,
                UserId = note.UserId,
            };
        }
    }
}
