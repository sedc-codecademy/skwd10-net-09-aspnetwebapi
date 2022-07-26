﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notes.Contracts.DTOs;
using Notes.Contracts.DTOs.UserDtos;
using Notes.Contracts.Services;
using NotesApi.Controllers;
using System.Threading.Tasks;

namespace Notes.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void RegisterUserWithEmptyRegisterDtoShouldNotCreateUser()
        {
            //Arrange
            INoteService noteService = Mock.Of<INoteService>();
            IUserService userService = Mock.Of<IUserService>();

            UsersController usersController = new UsersController(userService, noteService);
            RegisterUserDto registerUserDto = null;

            //Act
            IActionResult result = usersController.RegisterUser(registerUserDto);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task GetNoteShouldReturnNoteWhenIdIsPassed()
        {
            //Arrange
            NoteDto noteDto = new NoteDto()
            {
                Id = 1,
                Description = "Description1",
                Name = "Note1"
            };

            Mock<INoteService> noteService = new Mock<INoteService>();
            noteService.Setup(note => note.GetNoteAsync(1)).ReturnsAsync(noteDto);
            IUserService userService = Mock.Of<IUserService>();

            UsersController usersController = new UsersController(userService, noteService.Object);

            //Act
            ActionResult<NoteDto> result = await usersController.GetNote(noteDto.Id);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            OkObjectResult castedResult = (OkObjectResult)result.Result;
            NoteDto value = (NoteDto)castedResult.Value;
            Assert.AreEqual(value.Id, noteDto.Id);
            Assert.AreEqual(value.Name, noteDto.Name);
            Assert.AreEqual(value.Description, noteDto.Description);
        }
    }
}
