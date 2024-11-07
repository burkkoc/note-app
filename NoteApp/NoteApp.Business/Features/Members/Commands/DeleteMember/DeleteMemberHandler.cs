using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Business.Services;
using NoteApp.Core.Enums;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JWTService _jwtService;
        private readonly MemberWriteRepository _writeRepository;
        public DeleteMemberHandler(IHttpContextAccessor httpContextAccessor, JWTService jwtService, MemberWriteRepository writeRepository)
        {
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var principles = _jwtService.ValidateToken(_httpContextAccessor);
            var isAuthorized = principles.HasClaim(claim => claim.Type == "CanDelete" && claim.Value == "True");
            if (!isAuthorized)
                throw new UnauthorizedAccessException("You do NOT have permissions.");

            var removerUsername = _jwtService.GetUsername(principles);
            await _writeRepository.SoftRemoveAsync(request.MemberId.ToString(), removerUsername);
            await _writeRepository.SaveAsync();

            return true;
        }
    }
}
