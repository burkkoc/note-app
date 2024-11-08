using MediatR;
using NoteApp.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Commands.EditMember
{
    public class UpdateMemberCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
