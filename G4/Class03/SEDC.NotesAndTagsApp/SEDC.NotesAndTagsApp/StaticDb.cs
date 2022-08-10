using SEDC.NotesAndTagsApp.Models;
using SEDC.NotesAndTagsApp.Models.Enums;

namespace SEDC.NotesAndTagsApp
{
    public static class StaticDb
    {
        public static List<Note> Notes = new List<Note>()
        {
             new Note(){ Text = "Do Homework", Priority = Priority.High, Tags = new List<Tag>()
                {
                    new Tag(){ Name = "HomeWork", Color = "cyan"},
                    new Tag(){ Name = "SEDC", Color = "blue"}
                }
            },
            new Note(){ Text = "Drink more Water", Priority = Priority.Medium, Tags = new List<Tag>()
                {
                    new Tag(){ Name = "Healthy", Color = "orange"},
                    new Tag(){ Name = "Priority High", Color = "red"}
                }
            },
            new Note(){ Text = "Go to the gym", Priority = Priority.Low, Tags = new List<Tag>()
                {
                    new Tag(){ Name = "exercise", Color = "blue"},
                    new Tag(){ Name = "Priority Medium", Color = "yellow"}
                }
            }
        };
    }
}
