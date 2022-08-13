using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL
{
    public static class StaticDB
    {
        public static List<UserDto> Users = new List<UserDto>();
        public static List<NoteDto> Notes = new List<NoteDto>();
    }
}
