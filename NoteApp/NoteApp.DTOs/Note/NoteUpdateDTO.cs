﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DTOs.Note
{
    public class NoteUpdateDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;

    }
}
