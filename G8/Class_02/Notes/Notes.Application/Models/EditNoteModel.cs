﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Models
{
    public class EditNoteModel
        : CreateNoteModel
    {
        public int Id { get; set; }
    }
}
