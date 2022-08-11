using Notes.Application.Models;
using Notes.Domain.Models;

namespace Notes.Application.Mapper
{
    public static class UserMapper
    {
        public static UserModel ToModel(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                LastName = user.LastName,
                Name = user.FirstName,
                UserName = user.Username
            };
        }
    }
}
