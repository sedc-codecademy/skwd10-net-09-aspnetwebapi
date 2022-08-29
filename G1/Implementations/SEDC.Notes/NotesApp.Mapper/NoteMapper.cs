using NotesApp.DataModels;
using NotesApp.InerfaceModels.Enums;
using NotesApp.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Mapper
{
    public static class NoteMapper
    {
        public static NoteDto ToNoteDto(NoteModel model) 
        {
            return new NoteDto
            {
                Color = model.Color,
                Text = model.Text,
                Tag = (int)model.Tag,
                UserId = model.UserId
            };
        }

        public static NoteModel ToNoteModel(NoteDto model) 
        {
            return new NoteModel
            {
                Id = model.Id,
                Color = model.Color,
                Text = model.Text,
                Tag = (TagType)model.Tag,
                UserId = model.UserId
            };
        }
    }
}
