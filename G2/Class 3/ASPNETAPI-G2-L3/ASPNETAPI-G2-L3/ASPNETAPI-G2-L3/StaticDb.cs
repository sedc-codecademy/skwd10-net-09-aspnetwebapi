using ASPNETAPI_G2_L3.Entities;

namespace ASPNETAPI_G2_L3
{
    public static class StaticDb
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Bojan",
                LastName = "Damcevski",
                Username = "bdamcevski",
                Password = "123"
            },
            new User
            {
                Id = 2,
                FirstName = "Mihajlo",
                LastName = "Dimovski",
                Username = "mdimovski",
                Password = "123"
            }
        };

        public static List<Note> Notes = new List<Note>()
        {
            new Note(){ Id =1, Text = "Do Homework", Color = "blue", Tags = new List<Tag>()
                {
                    new Tag(){ Id = 1, Name = "HomeWork", Color = "cyan"},
                    new Tag(){ Id = 2, Name = "SEDC", Color = "blue"}
                },
                User = Users.First()
            },
            new Note(){ Id=2, Text = "Drink more Water", Color = "red", Tags = new List<Tag>()
                {
                    new Tag(){ Id = 3, Name = "Healthy", Color = "orange"},
                    new Tag(){ Id = 4, Name = "Priority High", Color = "red"},
                    new Tag(){ Id = 7, Name = "Healthy lifestyle", Color = "green"}
                },
                User = Users.Last()
            },
            new Note(){ Id =3, Text = "Go to the gym", Color = "green", Tags = new List<Tag>()
                {
                    new Tag(){ Id = 5, Name = "exercise", Color = "blue"},
                    new Tag(){ Id = 6, Name = "Priority Medium", Color = "yellow"},
                    new Tag(){ Id = 7, Name = "Healthy lifestyle", Color = "green"}
                },
                User = Users.First()
            }
        };
    }
}
