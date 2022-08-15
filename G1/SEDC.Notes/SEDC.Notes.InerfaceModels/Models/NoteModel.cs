using SEDC.Notes.InerfaceModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Notes.InerfaceModels.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public TagType Tag { get; set; }

        public int UserId { get; set; }
    }
}
