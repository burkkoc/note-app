using MediatR;
using NoteApp.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<MemberDTO>
    {
        public string Id { get; }
        public GetUserByIdQuery(string userId)
        {
            Id = userId;
        }
    }

}
