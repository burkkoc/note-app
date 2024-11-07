using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NoteApp.DataAccessEFCore.Repositories;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.Entities.DbSets;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Users.Commands
{
    public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, bool>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MemberWriteRepository _writeRepository;
        private readonly IMapper _mapper;
        public CreateMemberHandler(UserManager<IdentityUser> userManager, MemberWriteRepository writeRepository, IMapper mapper)
        {
            _userManager = userManager;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<IdentityUser>(request);
            var member = _mapper.Map<Member>(request);

            member.IdentityId = identityUser.Id;
            member.CreatedBy = "System";

            bool memberResult = await _writeRepository.AddAsync(member);
            if (!memberResult)
                return false;

            var identityResult = await _userManager.CreateAsync(identityUser);
            if (!identityResult.Succeeded)
                throw new Exception("Member creation failed: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));



            await _writeRepository.SaveAsync();

            return true;
                
        }
    }
}
