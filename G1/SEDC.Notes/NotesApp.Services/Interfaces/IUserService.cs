using SEDC.Notes.InerfaceModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterModel model);
        List<UserModel> GetAllUsers();
    }
}
