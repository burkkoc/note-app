using AutoMapper;
using MediatR;
using NoteApp.Business.Features.Users.Queries;
using NoteApp.Core.Repositories.Interfaces;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.DTOs.Member;
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
        private readonly IMemberReadRepository _memberReadRepository;
        private readonly IMapper _mapper;
        public GetMemberByIdHandler(IMemberReadRepository memberReadRepository, IMapper mapper)
        {
            _memberReadRepository = memberReadRepository;
            _mapper = mapper;
        }


        public async Task<MemberDTO> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _memberReadRepository.GetByIdAsync(request.Id.ToString());
            if (user == null) throw new Exception("User not found.");
            return _mapper.Map<MemberDTO>(user);

        }
    }
}
