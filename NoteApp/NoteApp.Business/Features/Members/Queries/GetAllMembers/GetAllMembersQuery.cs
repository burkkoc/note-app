using MediatR;
using NoteApp.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Queries.GetAllMembers
{
    public class GetAllMembersQuery : IRequest<List<MemberListDTO>>
    {

    }
}
