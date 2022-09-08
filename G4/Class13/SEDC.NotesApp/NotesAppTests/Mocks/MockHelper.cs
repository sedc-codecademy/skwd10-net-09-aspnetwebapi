using Moq;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppTests.Mocks
{
    public static class MockHelper
    {

        public static IRepository<User> MockUserRepository()
        {
            List<User> users = new List<User>() {
                new User()
                {
                    Id = 1,
                    Age = 20,
                    FirstName= "Goce",
                    LastName = "Kabov",
                    Username = "Goce_Kabov"
                }
            };

            var mockUserRepository = new Mock<IRepository<User>>();
            mockUserRepository.Setup(x => x.GetAll()).Returns(users);
            mockUserRepository.Setup(x => x.Add(It.IsAny<User>())).Callback((User user) => users.Add(user));
            mockUserRepository.Setup(x => x.Update(It.IsAny<User>())).Callback((User user) => users[users.IndexOf(user)] = user);
            mockUserRepository.Setup(x => x.Delete(It.IsAny<User>())).Callback((User user) => users.Remove(user));
            mockUserRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => users.SingleOrDefault((user) => user.Id == id));

            return mockUserRepository.Object;
        }



        public static IRepository<Note> MockNoteRepository()
        {
            List<Note> notes = new List<Note>()
                {
                    new Note()
                    {
                        Id = 1,
                        UserId = 1,
                        Priority = SEDC.NotesApp.Domain.Enums.Priority.High,
                        Tag = SEDC.NotesApp.Domain.Enums.Tag.Health,
                        Text = "Do something"
                    },
                    new Note()
                    {
                        Id = 2,
                        UserId = 1,
                        Priority = SEDC.NotesApp.Domain.Enums.Priority.Medium,
                        Tag = SEDC.NotesApp.Domain.Enums.Tag.SocialLife,
                        Text = "Do something else!"
                    }
                };

            var mockNotesRepository = new Mock<IRepository<Note>>();
            mockNotesRepository.Setup(x => x.GetAll()).Returns(notes);
            mockNotesRepository.Setup(x => x.Add(It.IsAny<Note>())).Callback((Note note) => notes.Add(note));
            mockNotesRepository.Setup(x => x.Update(It.IsAny<Note>())).Callback((Note note) => notes[notes.IndexOf(note)] = note);
            mockNotesRepository.Setup(x => x.Delete(It.IsAny<Note>())).Callback((Note note) => notes.Remove(note));
            mockNotesRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => notes.SingleOrDefault(note => note.Id == id));

            return mockNotesRepository.Object;
        }
    }
}
