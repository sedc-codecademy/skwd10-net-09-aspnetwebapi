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

    }
}
