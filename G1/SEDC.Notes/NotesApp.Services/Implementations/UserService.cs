using NotesApp.DAL;
using NotesApp.DAL.Repositories;
using NotesApp.DataModels;
using NotesApp.Mapper;
using NotesApp.Services.Interfaces;
using SEDC.Notes.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserDto> _userRepository;

        public UserService(IRepository<UserDto> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(RegisterModel model) 
        {
            var user = new UserDto
            {
                Username = model.Username,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            _userRepository.Add(user);
        }

        public List<UserModel> GetAllUsers() 
        {
            return _userRepository.GetAll()
                                  .Select(user => UserMapper.ToUserModel(user))
                                  .ToList();
        }

    }
}
