using NoteApp.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Entities.Base
{
    public class BaseNote : BaseEntity, INote
    {
        public string Content { get; set; } = null!;
        public string Title { get; set; } = null!;
    }
}
