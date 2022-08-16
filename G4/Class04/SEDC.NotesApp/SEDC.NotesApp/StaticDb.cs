﻿using SEDC.NotesApp.Models;

namespace SEDC.NotesApp
{
    public static class StaticDb
    {
        public static List<string> SimpleNotes = new List<string>
        {
            "Do the homework",
            "Go to the gym",
            "Buy bread"
        };

        public static List<Note> Notes = new List<Note>();
    }
}
