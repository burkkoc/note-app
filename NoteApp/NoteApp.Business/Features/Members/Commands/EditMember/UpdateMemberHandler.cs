using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Services;
using NoteApp.Core.Auth;
using NoteApp.Core.Enums;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.DTOs.User;
using NoteApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Commands.EditMember
{
    public class UpdateMemberHandler : IRequestHandler<UpdateMemberCommand, bool>
    {

        private readonly JWTService _jwtService;
        private readonly MemberWriteRepository _writeRepository;
        private readonly MemberReadRepository _readRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public UpdateMemberHandler(MemberWriteRepository writeRepository, JWTService jwtService, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<IdentityUser> userManager, MemberReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userManager = userManager;
            _readRepository = readRepository;
        }
        public async Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var claims = _jwtService.ValidateToken(_httpContextAccessor);
            var isAuthorized = claims.HasClaim(claim => claim.Type == CustomClaims.CanEdit && claim.Value == ClaimStates.Yes.ToString());
            if (!isAuthorized)
                throw new UnauthorizedAccessException("You do NOT have permission.");

            var modifierUsername = _jwtService.GetUsername(claims);
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
