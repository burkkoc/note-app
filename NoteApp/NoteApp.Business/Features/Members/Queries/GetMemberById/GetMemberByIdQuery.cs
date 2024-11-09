using MediatR;
using NoteApp.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Users.Queries
{
    public class GetMemberByIdQuery : IRequest<MemberDTO>
    {
        public Guid Id { get; }
        public GetMemberByIdQuery(Guid userId)
        {
            Id = userId;
        }
    }

}
