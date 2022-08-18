using ASPNETAPI_G2_L6.Models;
using ASPNETAPI_G2_L6.Models.Enums;

namespace ASPNETAPI_G2_L6.Database.Seeds
{
    public static class NotesSeed
    {
        public static List<Note> NOTES = new List<Note>()
        {
            new Note()
            {
                Id = -1,
                Color = "Red",
                Tag = TagType.Work,
                Text = "Text 123",
                UserId = UsersSeed.USERS.First().Id
            },
            new Note()
            {
                Id = -2,
                Color = "Green",
                Tag = TagType.Home,
                Text = "Text 234",
                UserId = UsersSeed.USERS.First().Id
            },
            new Note()
            {
                Id = -3,
                Color = "Yellow",
                Tag = TagType.Other,
                Text = "Text Office 4444",
                UserId = UsersSeed.USERS.First().Id
            },
            new Note()
            {
                Id = -4,
                Color = "Yellow",
                Tag = TagType.Other,
                Text = "Text Office 4444",
                UserId = UsersSeed.USERS.Last().Id
            }
        };
    }
}
