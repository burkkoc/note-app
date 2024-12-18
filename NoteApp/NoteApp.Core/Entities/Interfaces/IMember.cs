﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core.Entities.Interfaces
{
    public interface IMember
    {
        string? IdentityUserId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string? PhoneNumber { get; set; }
        bool Gender { get; set; }
    }
}
