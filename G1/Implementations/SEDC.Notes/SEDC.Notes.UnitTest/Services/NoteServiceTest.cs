using AutoMapper;
using Moq;
using NotesApp.DAL;
using NotesApp.DataModels;
using NotesApp.InerfaceModels.Models;
using NotesApp.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Notes.UnitTest.Services
{
    [TestClass]
    public class NoteServiceTest
    {
        private readonly Mock<IRepository<NoteDto>> repository = new Mock<IRepository<NoteDto>>();
        private readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private readonly NoteService service;
        public NoteServiceTest()
        {
            service = new NoteService(repository.Object, mapper.Object);
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnNote()
        {
            // Arrange
            var note = new NoteDto
            {
                Id = 1,
                UserId = 1
            };
            repository.Setup(x => x.GetById(note.Id, note.UserId)).Returns(note);
            var expected = new NoteModel()
            {
                Id = 1
            };

            // Act
            var result = service.GetById(note.Id, note.UserId);

            //Assert
            Assert.AreEqual(expected.Id, result.Id);
        }

    }
}
