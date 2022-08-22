using NotesApp.DataModels;
using SEDC.Notes.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(UserModel model) 
        {
            return new UserDto
            {
                Username = model.Username,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public static UserModel ToUserModel(UserDto model)
        {
            return new UserModel
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = model.Password,
                Notes = model.NoteList.Select(note => NoteMapper.ToNoteModel(note)).ToList()
            };
        }
    }
}
