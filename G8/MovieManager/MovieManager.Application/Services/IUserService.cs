using MovieManager.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public interface IUserService
    {
        void Register(RegisterDto model);
        TokenDto Autenticate(LoginDto login);
        List<UserDto> GetUsers();
    }
}
