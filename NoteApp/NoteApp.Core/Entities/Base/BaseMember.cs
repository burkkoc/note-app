using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteApp.Core.Entities.Interfaces;
using NoteApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Entities.Base
{
    [Index(nameof(Email), IsUnique = true)]
    public class BaseMember : BaseEntity, IMember
    {
        [ForeignKey("IdentityId")]
        public string IdentityUserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public virtual IdentityUser IdentityUser { get; set; } = null!;
        

    }
}
