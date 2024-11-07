using MediatR;
using NoteApp.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Users.Commands
{
    public class CreateMemberCommand : IRequest<bool>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool Gender { get; set; }

        
    }
}
