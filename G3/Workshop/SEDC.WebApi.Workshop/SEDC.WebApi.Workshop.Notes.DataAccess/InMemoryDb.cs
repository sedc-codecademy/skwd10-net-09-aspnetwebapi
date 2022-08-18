using SEDC.WebApi.Workshop.Notes.DataModels.Models;

namespace SEDC.WebApi.Workshop.Notes.DataAccess
{
    public static class InMemoryDb
    {
        public static List<User> Users { get; set; } = new List<User>
        {
            new User()
            {
                FirstName = "Trajan",
                LastName = "Stevkovski",
                Id = 1,
                NoteList = new List<Note>(),
                Password = "12345",
                Username = "ts123"
            },
        };
        public static List<Note> Notes { get; set; } = new List<Note>
        {
            new Note
            {
                Id = 1,
                Color = "Orange",
                Tag = 1,
                Text = "Hello",
                User = new User()
                        {
                            FirstName = "Trajan",
                            LastName = "Stevkovski",
                            Id = 1,
                            NoteList = new List<Note>(),
                            Password = "12345",
                            Username = "ts123"
                        },
                UserId = 1
            },
            new Note
            {
                Id = 2,
                Color = "Green",
                Tag = 1,
                Text = "Hello",
                User = new User()
                        {
                            FirstName = "Trajan",
                            LastName = "Stevkovski",
                            Id = 1,
                            NoteList = new List<Note>(),
                            Password = "12345",
                            Username = "ts123"
                        },
                UserId = 1
            }
        };
    }
}
