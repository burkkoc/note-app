using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DTOs.Note
{
    public class NoteCreateDTO
    {
        public string Content { get; set; } = null!;
        public string Title { get; set; } = null!;
    }
}
