using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Services;
using NoteApp.Core.Auth;
using NoteApp.Core.Enums;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.Entities.DbSets;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Commands.EditMember
{
    public class UpdateMemberHandler : IRequestHandler<UpdateMemberCommand, bool>
    {

        private readonly IMemberWriteRepository _writeRepository;
        private readonly IMemberReadRepository _readRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public UpdateMemberHandler(IMemberWriteRepository writeRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<IdentityUser> userManager, IMemberReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _readRepository = readRepository;
        }
        public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {

            var modifierUsername = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException();
            var member = await _readRepository.GetByIdAsync(request.Id.ToString());

            if(member == null)
                throw new ArgumentNullException(nameof(member));

            var identityUser = member.IdentityUser;
            _mapper.Map(request, member);
            _mapper.Map(request, identityUser);

            await _writeRepository.UpdateAsync(member, modifierUsername);
            await _writeRepository.SaveAsync();
            await _userManager.UpdateAsync(identityUser);
            
            return true;
        }
    }
}
