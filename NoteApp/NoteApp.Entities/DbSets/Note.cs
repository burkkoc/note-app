using Microsoft.AspNetCore.Identity;
using NoteApp.Core.Entities.Base;
using NoteApp.Core.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Entities.DbSets
{
    public class Note : BaseNote
    {
        [ForeignKey("MemberId")]
        public Guid MemberId { get; set; }
        public virtual Member Member { get; set; } = null!;


    }
}
