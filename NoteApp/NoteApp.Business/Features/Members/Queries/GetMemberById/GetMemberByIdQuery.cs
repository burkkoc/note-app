using MediatR;
using NoteApp.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Users.Queries
{
    public class GetMemberByIdQuery : IRequest<MemberDTO>
    {
        public string Id { get; }
        public GetMemberByIdQuery(string userId)
        {
            Id = userId;
        }
    }

}
