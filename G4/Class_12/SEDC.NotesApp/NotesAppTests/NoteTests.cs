using NotesAppTests.FakeRepositories;
using SEDC.NotesApp.Dtos;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppTests
{
    [TestClass]
    public class NoteTests
    {
        [TestMethod]
        public void AddNote_InvalidUserId_Exception()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());
            var newNote = new AddNoteDto()
            {
                Priority = SEDC.NotesApp.Domain.Enums.Priority.Low,
                Tag = SEDC.NotesApp.Domain.Enums.Tag.Work,
                Text = "Do your work!",
                UserId = 2
            };

            // Assert
            Assert.ThrowsException<NoteDataException>(() => noteService.AddNote(newNote));

        }

        [TestMethod]
        public void AddNote_EmptyText_Exception()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());
            var newNote = new AddNoteDto()
            {
                Priority = SEDC.NotesApp.Domain.Enums.Priority.Low,
                Tag = SEDC.NotesApp.Domain.Enums.Tag.Work,
                Text = "",
                UserId = 1
            };

            // Assert
            Assert.ThrowsException<NoteDataException>(() => noteService.AddNote(newNote));

        }

        [TestMethod]
        public void AddNote_LargerText_Exception()
        {
            // Arrange
            INoteService noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());
            var newNote = new AddNoteDto()
            {
                Priority = SEDC.NotesApp.Domain.Enums.Priority.Low,
                Tag = SEDC.NotesApp.Domain.Enums.Tag.Work,
                Text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                UserId = 1
            };

            // Assert
            Assert.ThrowsException<NoteDataException>(() => noteService.AddNote(newNote));

        }


        [TestMethod]
        public void GetAllNotes_Count()
        {
            var noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());
            var expectedCount = 2;

            var result = noteService.GetAllNotes();

            Assert.AreEqual(expectedCount, result.Count);
        }

        [TestMethod]
        public void GetNoteById_InvalidId_Exception()
        {
            var noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());


            Assert.ThrowsException<NoteNotFoundException>(() => noteService.GetById(3));
        }

        [TestMethod]
        public void GetNoteById_ValidUser_NoteDto()
        {
            var noteService = new NoteService(new FakeNotesRepository(), new FakeUserRepository());
            var expectedNoteText = "Do something";

            var result = noteService.GetById(1);

            Assert.AreEqual(expectedNoteText, result.Text);

        }
    }
}
