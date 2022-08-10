using UserApp.Models;

namespace UserApp
{
    public class StaticDB
    {
        public static List<User> Users = new List<User>
        {
            new User 
            {
                Id = 1,
                Name = "Viktor",
                Password = "123456"
            },
            new User
            {
                Id = 2,
                Name = "Igor",
                Password = "igor123"
            },
            new User
            {
                Id = 3,
                Name = "Petar",
                Password = "444444"
            }
        };


    }
}
