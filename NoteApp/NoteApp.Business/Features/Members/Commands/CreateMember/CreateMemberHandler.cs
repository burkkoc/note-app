using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.Business.Services;
using NoteApp.Core.Auth;
using NoteApp.Core.Enums;
using NoteApp.DataAccessEFCore.Repositories;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.Entities.DbSets;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Users.Commands
{
    public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, bool>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemberWriteRepository _writeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateMemberHandler(UserManager<IdentityUser> userManager, IMemberWriteRepository writeRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _writeRepository = writeRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {

            var identityUser = _mapper.Map<IdentityUser>(request);
            var member = _mapper.Map<Member>(request);

            member.IdentityUserId = identityUser.Id;
            member.CreatedBy = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException();

            string pass = /*PasswordService.GenerateRandomPassword();*/"Asd123$.";

            var claims = new List<Claim>
            {
                new Claim(CustomClaims.CanDeleteOwnNote, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanEditOwnNote, ClaimStates.Yes.ToString()),
                new Claim(CustomClaims.CanCreateOwnNote, ClaimStates.Yes.ToString())
            };



            var identityResult = await _userManager.CreateAsync(identityUser, pass);
            bool memberResult = await _writeRepository.AddAsync(member);
            if (!memberResult)
                return false;

            if (!identityResult.Succeeded)
                throw new Exception("Member creation failed: " + string.Join(", ", identityResult.Errors.Select(e => e.Description)));



            await _userManager.AddClaimsAsync(identityUser, claims);
            await _writeRepository.SaveAsync();

            return true;

        }
    }
}
