using AutoMapper;
using MediatR;
using NoteApp.Business.Features.Users.Queries.GetUserById;
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
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, MemberDTO>
    {
        private readonly IReadRepository<Member> _userReadRepository;
        private readonly IMapper _mapper;
        public GetUserByIdHandler(IReadRepository<Member> userReadRepository, IMapper mapper)
        {
            _userReadRepository = userReadRepository;
            _mapper = mapper;
        }


        public async Task<MemberDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userReadRepository.GetByIdAsync(request.Id);
            if (user == null) throw new Exception("User not found.");
            return _mapper.Map<MemberDTO>(user);

        }
    }
}
