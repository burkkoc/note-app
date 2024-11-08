using MediatR;
using Microsoft.AspNetCore.Http;
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
        
        public DeleteMemberHandler(IHttpContextAccessor httpContextAccessor, IMemberWriteRepository writeRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _writeRepository = writeRepository;
        }

        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var removerUsername = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException();
            
            await _writeRepository.SoftRemoveAsync(request.MemberId.ToString(), removerUsername);
            await _writeRepository.SaveAsync();

            return true;
        }
    }
}
