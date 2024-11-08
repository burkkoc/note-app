using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberCommand : IRequest<bool>
    {
        public Guid MemberId { get; set; }
        public DeleteMemberCommand(Guid id)
        {
            MemberId = id;
        }

    }
}
