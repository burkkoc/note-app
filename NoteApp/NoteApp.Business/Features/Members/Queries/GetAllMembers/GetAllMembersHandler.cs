using AutoMapper;
using MediatR;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Queries.GetAllMembers
{
    public class GetAllMembersHandler : IRequestHandler<GetAllMembersQuery, List<MemberListDTO>>
    {
        private readonly IMemberReadRepository _memberReadRepository;
        private readonly IMapper _mapper;
        public GetAllMembersHandler(IMemberReadRepository memberReadRepository, IMapper mapper)
        {
            _memberReadRepository = memberReadRepository;
            _mapper = mapper;
        }


        public async Task<List<MemberListDTO>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            var listOfMembers = await _memberReadRepository.GetAllAsync();
            List<MemberListDTO> memberList = new();
            foreach (var member in listOfMembers)
            {
                var memberDTO = _mapper.Map<MemberListDTO>(member);
                memberList.Add(memberDTO);
            }
            return memberList;
        }
    }
}
