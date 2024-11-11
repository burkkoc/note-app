using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NoteApp.DataAccessEFCore.Repositories.UserRepositories;
using NoteApp.Interfaces.Repositories;
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
        private readonly IMemberWriteRepository _writeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemberReadRepository _memberReadRepository;

        public DeleteMemberHandler(IHttpContextAccessor httpContextAccessor, IMemberWriteRepository writeRepository, UserManager<IdentityUser> userManager, IMemberReadRepository memberReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _writeRepository = writeRepository;
            _userManager = userManager;
            _memberReadRepository = memberReadRepository;
        }

        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var removerUsername = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException();

            var member = await _memberReadRepository.GetByIdAsync(request.MemberId.ToString());
            if (member.Email == "admin@noteapp.com" || member.Email == "ADMIN@NOTEAPP.COM")
                throw new Exception("Admin cannot be deleted.");

            await _writeRepository.SoftRemoveAsync(request.MemberId.ToString(), removerUsername);
            await _writeRepository.SaveAsync();

            return true;
        }
    }
}
