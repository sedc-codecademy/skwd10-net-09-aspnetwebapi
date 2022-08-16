using NotesApp.DAL.Repositories;
using NotesApp.DataModels;
using SEDC.Notes.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Implementations
{
    public class UserService
    {
        private readonly UserStaticDbRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserStaticDbRepository();
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
            var users = _userRepository.GetAll();

            var response = new List<UserModel>();

            foreach (var user in users)
            {
                var userModel = new UserModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Password = user.Password
                };

                response.Add(userModel);
            }

            return response;
        }

    }
}
