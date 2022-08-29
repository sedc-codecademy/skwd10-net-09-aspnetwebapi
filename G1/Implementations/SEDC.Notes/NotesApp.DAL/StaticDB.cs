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
        public static int UserIdCounter = 0;
        public static List<UserDto> Users = new List<UserDto>()
        {
            new UserDto
            {
                Id = 0,
                FirstName = "Viktor",
                LastName = "Jakovlev",
                Username = "vjakovlev",
                Password = "P@ssw0rd"
            }
        };

        public static int NoteIdCounter = 0;
        public static List<NoteDto> Notes = new List<NoteDto>();
    }
}
