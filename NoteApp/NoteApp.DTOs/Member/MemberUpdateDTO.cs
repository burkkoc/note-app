﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.DTOs.User
{
    public class MemberUpdateDTO
    {
        public Guid Id { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
