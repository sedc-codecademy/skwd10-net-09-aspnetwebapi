﻿using NotesAndTagsApp.Models.Enums;

namespace NotesAndTagsApp.DTOs
{
    public class UpdateNoteDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public PriorityEnum Priority { get; set; }
        public int UserId { get; set; }
        public List<int> TagIds { get; set; }
    }
}
