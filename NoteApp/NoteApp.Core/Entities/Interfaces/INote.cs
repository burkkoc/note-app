using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Entities.Interfaces
{
    public interface INote
    {
         string Content { get; set; } 
         string Title { get; set; } 
    }
}
