﻿using Notes.Application.Models;

namespace Notes.Application.Services
{
    public interface INoteService
    {
        public NoteModel GetNote(int id);

        public IEnumerable<NoteModel> GetNotes();

        public NoteModel CreateNote(CreateNoteModel model, int userId);
    }
}
