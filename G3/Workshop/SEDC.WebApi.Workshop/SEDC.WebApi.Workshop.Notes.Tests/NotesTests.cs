using SEDC.WebApi.Workshop.Notes.Common.Exceptions;
using SEDC.WebApi.Workshop.Notes.DataAccess;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using SEDC.WebApi.Workshop.Notes.Sevices;

namespace SEDC.WebApi.Workshop.Notes.Tests
{
    [TestClass]
    public class NotesTests
    {
        private Mock<IRepository<Note>> _noteRepository;
        private Mock<IRepository<User>> _userRepository;

        public NotesTests()
        {
            _noteRepository = new Mock<IRepository<Note>>();
            _userRepository = new Mock<IRepository<User>>();
        }

        [TestMethod]
        public void GetUserNotes_GetValidResponse()
        {
            // Arange
            var notes = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Color = "green",
                    Tag = 2,
                    Text = "Unit testing",
                    UserId = 1
                }
            };
            var userId = 1;

            _noteRepository.Setup(x => x.FilterBy(It.IsAny<Func<Note, bool>>()))
                .Returns(notes);

            var service = new NoteService(_noteRepository.Object, _userRepository.Object);

            // Act
            var notesDtos = service.GetUserNotes(userId);

            // Assert
            Assert.AreEqual(notes.Count, notesDtos.Count());
        }

        [TestMethod]
        public void GetNote_InvalidResponse()
        {
            // Arange
            var noteId = 1;
            var userId = 2;

            _noteRepository.Setup(x => x.FilterBy(It.IsAny<Func<Note, bool>>()))
                .Returns(new List<Note>());

            var service = new NoteService(_noteRepository.Object, _userRepository.Object);

            // Act
            // Assert
            Assert.ThrowsException<NoteException>(() =>
            {
                service.GetNote(noteId, userId);
            });

            // TODO:
        }

        [TestMethod]
        public void GetNote_ValidResponse()
        {
            // Arange
            var notes = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Color = "green",
                    Tag = 2,
                    Text = "Unit testing",
                    UserId = 1
                }
            };
            var noteId = 1;
            var userId = 1;

            _noteRepository.Setup(x => x.FilterBy(It.IsAny<Func<Note, bool>>()))
                .Returns(notes);

            var service = new NoteService(_noteRepository.Object, _userRepository.Object);

            // Act
            var note = service.GetNote(noteId, userId);

            // Assert
            Assert.IsNotNull(note);
            Assert.AreEqual(noteId, note.Id);
            Assert.AreEqual(userId, note.UserId);
        }

        [TestMethod]
        public void DeleteNote_NotValidResponse()
        {
            // Arange
            var noteId = 1;
            var userId = 2;

            _noteRepository.Setup(x => x.FilterBy(It.IsAny<Func<Note, bool>>()))
                .Returns(new List<Note>());

            var service = new NoteService(_noteRepository.Object, _userRepository.Object);

            // Act
            // Assert
            var response = Assert.ThrowsException<NoteException>(() =>
            {
                service.DeleteNote(noteId, userId);
            });
            Assert.AreEqual("Note not found", response.Message);
        }

        [TestMethod]
        public void DeleteNote_ValidResponse()
        {
            // Arange
            var notes = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Color = "green",
                    Tag = 2,
                    Text = "Unit testing",
                    UserId = 1
                }
            };
            var noteId = 1;
            var userId = 1;
            var expectedRecords = 0;

            _noteRepository.Setup(x => x.FilterBy(It.IsAny<Func<Note, bool>>()))
                .Returns(notes);
            _noteRepository.Setup(x => x.Delete(It.IsAny<Note>()))
                .Callback(() =>
                {
                    notes.Remove(notes[0]);
                });

            var service = new NoteService(_noteRepository.Object, _userRepository.Object);

            // Act
            service.DeleteNote(noteId, userId);

            // Assert

            Assert.AreEqual(expectedRecords, notes.Count);
            Assert.IsFalse(notes.Any());
        }

        [TestMethod]
        public void AddNote_InvalidResponse()
        {
            // Arrange
            User user = null;
            var request = new ServiceModels.NotesModels.CreateNote
            {
                Color = "Green",
                Tag = 2,
                Text = "Green note"
            };
            var userId = 1;

            var expectedMessage = "User not found";

            _userRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(user);

            var service = new NoteService(_noteRepository.Object, _userRepository.Object);

            // Act
            // Assert

            var ex = Assert.ThrowsException<UserException>(() =>
            {
                service.AddNote(request, userId);
            });
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        public void AddNote_ValidResponse()
        {
            // Arrange
            User user = new User
            {
                Id = 1,
                FirstName = "Trajan",
                LastName = "Stevkovski",
                Username = "stevt",
                Password = "asdzxc123"
            };

            var notes = new List<Note>
            {
                new Note
                {
                    Id = 1,
                    Color = "green",
                    Tag = 2,
                    Text = "Unit testing",
                    UserId = 1
                }
            };

            var request = new ServiceModels.NotesModels.CreateNote
            {
                Color = "Green",
                Tag = 2,
                Text = "Green note"
            };

            var note = new Note
            {
                Id = 2,
                UserId = 1,
                Color = "Green",
                Tag = 2,
                Text = "Green note"
            };

            var expectedRecords = 2;

            _userRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(user);
            _noteRepository.Setup(x => x.Insert(It.IsAny<Note>()))
                .Callback(() =>
                {
                    notes.Add(note);
                });

            var service = new NoteService(_noteRepository.Object, _userRepository.Object);

            // Act
            service.AddNote(request, user.Id);

            // Assert
            Assert.AreEqual(expectedRecords, notes.Count);
            Assert.IsTrue(notes.Any());
            Assert.AreSame(note, notes[1]);
        }
    }
}
