using NoteApp.Core.Entities.Base;
using NoteApp.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Entities.DbSets
{
    public class Member : BaseUser
    {
        public virtual ICollection<Note>? Notes { get; set; }
    }
}
