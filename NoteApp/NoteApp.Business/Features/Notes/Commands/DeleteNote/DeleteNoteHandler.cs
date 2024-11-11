using MediatR;
using Microsoft.AspNetCore.Http;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Commands.DeleteNote
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INoteWriteRepository _writeRepository;
        public DeleteNoteHandler(INoteWriteRepository writeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _writeRepository = writeRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var removerUsername = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException();

            await _writeRepository.SoftRemoveAsync(request.Id.ToString(), removerUsername);
            await _writeRepository.SaveAsync();

            return true;
        }
    }
}
