using NotesApp.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        UserModel Authenticate(string username, string password);
        void Register(RegisterModel model);
        List<UserModel> GetAllUsers();
    }
}
