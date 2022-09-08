using ASPNETAPI_G2_L6.Models;

namespace ASPNETAPI_G2_L6.Database.Seeds
{
    public static class UsersSeed
    {
        public static List<User> USERS = new List<User>()
        {
            new User()
            {
                Id = -1,
                FirstName = "Mihajlo",
                LastName = "Dimovski",
                Username = "mikid123",
                Password = "sedc456"
            },
            new User()
            {   Id = -2, 
                FirstName = "Bojan",
                LastName = "Damcevski",
                Username = "bokid123",
                Password = "sedc456"
            }
        };
    }
}
