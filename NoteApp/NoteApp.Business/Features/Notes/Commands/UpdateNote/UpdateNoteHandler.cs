using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NoteApp.Business.Features.Members.Commands.EditMember;
using NoteApp.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly INoteReadRepository _noteReadRepository;
        private readonly INoteWriteRepository _noteWriteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateNoteHandler(IMapper mapper, INoteReadRepository noteReadRepository, INoteWriteRepository noteWriteRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _noteReadRepository = noteReadRepository;
            _noteWriteRepository = noteWriteRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            try
            {

            var updater = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new ArgumentNullException();
            var note = await _noteReadRepository.GetByIdAsync(request.Id.ToString());
            note.Content = request.Content;
            note.Title = request.Title;
            await _noteWriteRepository.UpdateAsync(note, updater);
            await _noteWriteRepository.SaveAsync();
            return true;
            }
            catch
            {
                return false;
            }
            throw new NotImplementedException();
        }
    }
}
