using AutoMapper;
using MediatR;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.DTOs.User;
using NoteApp.Entities.DbSets;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Users.Handlers
{
    public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, MemberDTO>
    {
        private readonly IReadRepository<Member> _memberReadRepository;
        private readonly IMapper _mapper;
        public GetMemberByIdHandler(IReadRepository<Member> memberReadRepository, IMapper mapper)
        {
            _memberReadRepository = memberReadRepository;
            _mapper = mapper;
        }


        public async Task<MemberDTO> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _memberReadRepository.GetByIdAsync(request.Id);
            if (user == null) throw new Exception("User not found.");
            return _mapper.Map<MemberDTO>(user);

        }
    }
}
